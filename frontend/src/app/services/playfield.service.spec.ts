import { TestBed } from '@angular/core/testing';

import { PlayfieldService } from './playfield.service';

describe('PlayfieldService', () => {
  let service: PlayfieldService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlayfieldService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
