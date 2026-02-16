import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlaRingComponent } from './sla-ring.component';

describe('SlaRingComponent', () => {
  let component: SlaRingComponent;
  let fixture: ComponentFixture<SlaRingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SlaRingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SlaRingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
