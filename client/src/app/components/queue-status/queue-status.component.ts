import { Component, Input, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QueueDTO } from '../../core/models/dashboard.model';

@Component({
  selector: 'app-queue-status',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './queue-status.component.html',
})
export class QueueStatusComponent implements OnInit, OnDestroy {
  @Input() queues: QueueDTO[] = [];
  @Input() totalCount: number = 0;
  @Input() page: number = 1;
  @Input() pageSize: number = 10;

  @Output() pageChange = new EventEmitter<number>();

  private timer: any;

  ngOnInit() {
    this.timer = setInterval(() => {
      this.updateMaxWait();
    }, 1000);
  }

  ngOnDestroy() {
    if (this.timer) {
      clearInterval(this.timer);
    }
  }

  updateMaxWait() {
    this.queues.forEach(queue => {
      if (queue.oldestCallCreatedAt && queue.inQueue > 0) {
        const start = new Date(queue.oldestCallCreatedAt).getTime();
        const now = new Date().getTime();
        const diff = Math.max(0, now - start);

        const seconds = Math.floor((diff / 1000) % 60);
        const minutes = Math.floor((diff / (1000 * 60)) % 60);
        const hours = Math.floor((diff / (1000 * 60 * 60)));

        queue.maxWait = `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
      } else {
        queue.maxWait = "00:00:00";
      }
    });
  }

  get activeQueuesCount(): number {
    return this.queues.filter(q => q.inQueue > 0).length;
  }

  onPageChange(newPage: number) {
    this.pageChange.emit(newPage);
  }

  min(a: number, b: number): number {
    return Math.min(a, b);
  }
}
