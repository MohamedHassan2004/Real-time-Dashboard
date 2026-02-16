import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AgentDTO, AgentStatus, PagedResult } from '../../core/models/dashboard.model';

@Component({
  selector: 'app-agent-status',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './agent-status.component.html',
})
export class AgentStatusComponent implements OnInit, OnDestroy {
  @Input() agents: AgentDTO[] = [];
  @Input() totalCount: number = 0;
  @Input() page: number = 1;
  @Input() pageSize: number = 10;

  @Output() pageChange = new EventEmitter<number>();
  @Output() search = new EventEmitter<string>();

  searchTerm: string = '';
  AgentStatus = AgentStatus;
  private timer: any;

  ngOnInit() {
    this.timer = setInterval(() => {
      this.updateDurations();
    }, 1000);
  }

  ngOnDestroy() {
    if (this.timer) {
      clearInterval(this.timer);
    }
  }

  updateDurations() {
    this.agents.forEach(agent => {
      if (agent.lastStatusChange) {
        const start = new Date(agent.lastStatusChange).getTime();
        const now = new Date().getTime();
        const diff = Math.max(0, now - start);

        const seconds = Math.floor((diff / 1000) % 60);
        const minutes = Math.floor((diff / (1000 * 60)) % 60);
        const hours = Math.floor((diff / (1000 * 60 * 60)));

        agent.duration = `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
      }
    });
  }

  getStatusLabel(status: AgentStatus): string {
    switch (status) {
      case AgentStatus.Online: return 'Online';
      case AgentStatus.Idle: return 'Idle';
      case AgentStatus.NotReady: return 'Not Ready';
      default: return 'Unknown';
    }
  }

  onSearch() {
    this.search.emit(this.searchTerm);
  }

  onPageChange(newPage: number) {
    this.pageChange.emit(newPage);
  }

  sort(column: string) {
  }

  min(a: number, b: number): number {
    return Math.min(a, b);
  }
}
