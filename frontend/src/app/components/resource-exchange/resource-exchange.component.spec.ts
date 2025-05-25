import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResourceExchangeComponent } from './resource-exchange.component';

describe('ResourceExchangeComponent', () => {
  let component: ResourceExchangeComponent;
  let fixture: ComponentFixture<ResourceExchangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResourceExchangeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ResourceExchangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
