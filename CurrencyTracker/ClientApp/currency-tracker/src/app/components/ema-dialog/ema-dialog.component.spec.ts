import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmaDialogComponent } from './ema-dialog.component';

describe('EmaDialogComponent', () => {
  let component: EmaDialogComponent;
  let fixture: ComponentFixture<EmaDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmaDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmaDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
