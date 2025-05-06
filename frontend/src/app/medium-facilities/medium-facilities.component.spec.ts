import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediumFacilitiesComponent } from './medium-facilities.component';

describe('MediumFacilitiesComponent', () => {
  let component: MediumFacilitiesComponent;
  let fixture: ComponentFixture<MediumFacilitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MediumFacilitiesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MediumFacilitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
