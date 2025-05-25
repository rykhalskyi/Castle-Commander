import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LargeFacilitiesComponent } from './large-facilities.component';

describe('LargeFacilitiesComponent', () => {
  let component: LargeFacilitiesComponent;
  let fixture: ComponentFixture<LargeFacilitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LargeFacilitiesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LargeFacilitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
