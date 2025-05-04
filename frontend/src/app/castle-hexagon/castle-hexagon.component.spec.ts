import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CastleHexagonComponent } from './castle-hexagon.component';

describe('CastleHexagonComponent', () => {
  let component: CastleHexagonComponent;
  let fixture: ComponentFixture<CastleHexagonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CastleHexagonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CastleHexagonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
