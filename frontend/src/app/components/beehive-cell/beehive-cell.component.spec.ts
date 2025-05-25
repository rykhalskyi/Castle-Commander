import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BeehiveCellComponent } from './beehive-cell.component';

describe('BeehiveCellComponent', () => {
  let component: BeehiveCellComponent;
  let fixture: ComponentFixture<BeehiveCellComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BeehiveCellComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BeehiveCellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
