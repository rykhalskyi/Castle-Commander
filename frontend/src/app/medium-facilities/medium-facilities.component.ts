import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { Hexagon } from '../api-client';

@Component({
  selector: 'app-medium-facilities',
  imports: [],
  templateUrl: './medium-facilities.component.svg',
  styleUrl: './medium-facilities.component.scss'
})
export class MediumFacilitiesComponent implements OnInit, OnChanges {
  @Input() model: Hexagon | null = null;
  protected visibility: boolean[] = [false, false, false, false, false, false]

  ngOnInit(): void {
    this.setMediumFacilities();
  }

  ngOnChanges(): void {
    this.setMediumFacilities();
  }

  private setMediumFacilities(): void {
    this.visibility = [false, false, false, false, false, false];

    const mediumFacilities = this.model?.facilities?.filter(f => f.size === 2) ?? [];
    if (mediumFacilities.length === 0) {
      return;
    }
    for (let i = 0; i < mediumFacilities.length; i++) {
      this.visibility[mediumFacilities[i].startSector!] = true;
    }
  }

}
