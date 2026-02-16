import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './header.component.html',

})
export class HeaderComponent implements OnInit, OnDestroy {
  @Input() onlineCount: number = 0;
  @Input() idleCount: number = 0;
  @Input() notReadyCount: number = 0;

  currentDate: Date = new Date();
  private timer: any;

  ngOnInit() {
    this.timer = setInterval(() => {
      this.currentDate = new Date();
    }, 1000);
  }

  ngOnDestroy() {
    if (this.timer) {
      clearInterval(this.timer);
    }
  }
}
