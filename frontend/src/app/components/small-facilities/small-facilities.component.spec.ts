import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmallFacilitiesComponent } from './small-facilities.component';

describe('SmallFacilitiesComponent', () => {
  let component: SmallFacilitiesComponent;
  let fixture: ComponentFixture<SmallFacilitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SmallFacilitiesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SmallFacilitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
