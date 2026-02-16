import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-stats-grid',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stats-grid.component.html',
})
export class StatsGridComponent {
  @Input() offered: number = 0;
  @Input() answered: number = 0;
  @Input() inQueue: number = 0;
  @Input() abandoned: number = 0;
}
