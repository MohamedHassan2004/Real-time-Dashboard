import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sla-ring',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sla-ring.component.html',
})
export class SlaRingComponent implements OnChanges {
  @Input() percentage: number = 0;

  circumference = 2 * Math.PI * 40;
  dashOffset = 251.327; // Default full offset

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['percentage']) {
      this.updateOffset();
    }
  }

  private updateOffset() {
    // Ensure percentage is between 0 and 100
    const val = Math.max(0, Math.min(this.percentage, 100));
    const progress = val / 100;
    this.dashOffset = this.circumference * (1 - progress);
  }
}
