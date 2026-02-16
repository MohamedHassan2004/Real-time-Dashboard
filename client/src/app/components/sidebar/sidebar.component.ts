import { Component, Input } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { SlaRingComponent } from '../sla-ring/sla-ring.component';
import { StatsGridComponent } from '../stats-grid/stats-grid.component';
import { StatsDTO } from '../../core/models/dashboard.model';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, SlaRingComponent, StatsGridComponent, DatePipe],
  templateUrl: './sidebar.component.html',
})
export class SidebarComponent {
  @Input() stats: StatsDTO | null = null;
  lastUpdated: Date = new Date(); // Ideally this comes from the signal or a timestamp in StatsDTO
}
