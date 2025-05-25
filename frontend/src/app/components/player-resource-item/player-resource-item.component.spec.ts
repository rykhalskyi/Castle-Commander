import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerResourceItemComponent } from './player-resource-item.component';

describe('PlayerResourceItemComponent', () => {
  let component: PlayerResourceItemComponent;
  let fixture: ComponentFixture<PlayerResourceItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlayerResourceItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlayerResourceItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
