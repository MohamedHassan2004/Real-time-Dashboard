import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { AgentStatusComponent } from './components/agent-status/agent-status.component';
import { QueueStatusComponent } from './components/queue-status/queue-status.component';
import { DashboardService } from './core/services/dashboard.service';
import { SignalRService } from './core/services/signalr.service';
import { Subscription, combineLatest } from 'rxjs';
import { AgentDTO, QueueDTO, StatsDTO, AgentStatus, PagedResult } from './core/models/dashboard.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, HeaderComponent, SidebarComponent, AgentStatusComponent, QueueStatusComponent],
  template: `
    <div class="flex h-screen bg-gray-50 font-sans text-gray-900 overflow-hidden">
        <!-- Sidebar -->
        <app-sidebar [stats]="stats"></app-sidebar>

        <!-- Main Content -->
        <div class="flex-1 flex flex-col min-w-0">
            <!-- Header -->
            <app-header 
                [onlineCount]="onlineCount"
                [idleCount]="idleCount"
                [notReadyCount]="notReadyCount"
            ></app-header>

            <!-- Dashboard Content -->
            <main class="flex-1 p-6 overflow-auto">
                <div class="grid grid-cols-1 lg:grid-cols-2 gap-6 h-full">
                    <!-- Agent Status Panel -->
                    <div class="h-full">
                         <app-agent-status
                            [agents]="agents"
                            [totalCount]="agentsTotalCount"
                            [page]="agentsPage"
                            [pageSize]="agentsPageSize"
                            (pageChange)="onAgentPageChange($event)"
                            (search)="onAgentSearch($event)"
                         ></app-agent-status>
                    </div>

                    <!-- Queue Status Panel -->
                    <div class="h-full">
                        <app-queue-status
                            [queues]="queues"
                            [totalCount]="queuesTotalCount"
                            [page]="queuesPage"
                            [pageSize]="queuesPageSize"
                            (pageChange)="onQueuePageChange($event)"
                        ></app-queue-status>
                    </div>
                </div>
            </main>
        </div>
    </div>
  `
})
export class AppComponent implements OnInit, OnDestroy {
  dashboardService = inject(DashboardService);
  signalRService = inject(SignalRService);

  // Data
  stats: StatsDTO | null = null;
  agents: AgentDTO[] = [];
  queues: QueueDTO[] = [];

  // Metadata
  onlineCount = 0;
  idleCount = 0;
  notReadyCount = 0;

  // Pagination & Filtering
  agentsPage = 1;
  agentsPageSize = 10;
  agentsTotalCount = 0;
  agentFilter = '';

  queuesPage = 1;
  queuesPageSize = 10;
  queuesTotalCount = 0;

  // Subscriptions
  private subs: Subscription = new Subscription();

  ngOnInit() {
    this.initialLoad();
    this.setupSignalRSubscriptions();
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

  private initialLoad() {
    // Load Stats
    this.dashboardService.getStats().subscribe(res => {
      if (res.isSuccess) {
        this.stats = res.value;
      }
    });

    this.loadAgents();
    this.loadQueues();
  }

  private loadAgents() {
    this.dashboardService.getAgents(this.agentsPage, this.agentsPageSize, this.agentFilter)
      .subscribe(res => {
        if (res.isSuccess) {
          this.updateLocalAgents(res.value);
        }
      });
  }

  private loadQueues() {
    this.dashboardService.getQueues(this.queuesPage, this.queuesPageSize)
      .subscribe(res => {
        if (res.isSuccess) {
          this.updateLocalQueues(res.value);
        }
      });
  }

  // Handle SignalR Updates
  private setupSignalRSubscriptions() {
    // Stats Update
    this.subs.add(
      this.signalRService.stats$.subscribe(stats => {
        if (stats) {
          this.stats = stats;
        }
      })
    );

    // Agents Update - Real-time list replace (Simple strategy for this demo)
    // In a real app with pagination, we might only update if the current page is unaffected or use finer grained events.
    // For this requirement, let's assume the signal sends a list.
    // However, usually SignalR sends all items or single item updates.
    // The prompt says `ReceiveAgents -> AgentDTO[]`. If it sends ALL agents, we might need to handle client-side pagination or ignored if we only want paged views.
    // Let's assume SignalR updates override the current view for simplicity OR trigger a reload.
    // Given paginated API, SignalR sending ALL agents might be heavy. 
    // If SignalR sends just the updated agents, we merge.

    this.subs.add(
      this.signalRService.agents$.subscribe(updatedAgents => {
        if (updatedAgents && updatedAgents.length > 0) {
          // For the purpose of this dashboard, if we receive a full list or updates, 
          // we might just refresh our current paged view to be safe, 
          // or if the dataset is small enough (the prompt implies it might be), we could hold all in memory.
          // But sticking to the API pagination for the table, maybe just re-fetch?
          // Or if updatedAgents IS the full list, we slice it manually?
          // Let's assume for now we just refresh the current page to keep it in sync.
          this.loadAgents();
          this.updateCounts(updatedAgents); // Update header counts based on the stream
        }
      })
    );

    this.subs.add(
      this.signalRService.queues$.subscribe(updatedQueues => {
        if (updatedQueues && updatedQueues.length > 0) {
          this.loadQueues();
        }
      })
    );
  }

  // Helpers
  private updateLocalAgents(result: PagedResult<AgentDTO>) {
    this.agents = result.items;
    this.agentsTotalCount = result.totalCount;
    this.agentsPage = result.page;
    this.agentsPageSize = result.pageSize;

    // Also update header counts from this if we don't have a separate signal yet
    // But ideally we calculate counts from the full dataset. 
    // Since we are paginated, we rely on the backend or a separate stats call for totals.
    // For this specific UI, the header has counts.
    // Let's iterate the current page for now, or better, calculate from the stats if available (not in StatsDTO).
    // The instructions say "Status summary badges... Online: 0...". 
    // We'll calculate from the current loaded agents for now as a fallback.
    this.updateCounts(this.agents);
  }

  private updateLocalQueues(result: PagedResult<QueueDTO>) {
    this.queues = result.items;
    this.queuesTotalCount = result.totalCount;
    this.queuesPage = result.page;
    this.queuesPageSize = result.pageSize;
  }

  private updateCounts(agents: AgentDTO[]) {
    // This is imperfect with pagination (only counts visible agents). 
    // In a real app we'd need a separate endpoint or field in StatsDTO for "Total Online Agents".
    // I will implement a basic counter on the visible set for now.
    this.onlineCount = agents.filter(a => a.status === AgentStatus.Online).length;
    this.idleCount = agents.filter(a => a.status === AgentStatus.Idle).length;
    this.notReadyCount = agents.filter(a => a.status === AgentStatus.NotReady).length;
  }

  // Events
  onAgentPageChange(page: number) {
    this.agentsPage = page;
    this.loadAgents();
  }

  onAgentSearch(term: string) {
    this.agentFilter = term;
    this.agentsPage = 1;
    this.loadAgents();
  }

  onQueuePageChange(page: number) {
    this.queuesPage = page;
    this.loadQueues();
  }
}
