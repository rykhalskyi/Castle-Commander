import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Hexagon } from '../api-client';

@Component({
  selector: 'app-small-facilities',
  imports: [],
  templateUrl: './small-faclities.component.svg',
  styleUrl: './small-facilities.component.scss'
})
export class SmallFacilitiesComponent implements OnInit, OnChanges {


  @Input() model: Hexagon | null = null;
  protected visibility: boolean[] = [false, false, false, false, false, false]
     
  ngOnInit(): void {
    this.setSmallFacilities();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.setSmallFacilities();
  }
  
  private setSmallFacilities(): void {
    this.visibility = [false, false, false, false, false, false];

    const smallFacilities = this.model?.facilities?.filter(f => f.size === 1) ?? [];
    if (smallFacilities.length === 0) {
      return;
    }
    for (let i = 0; i < smallFacilities.length; i++) {
      this.visibility[smallFacilities[i].startSector!] = true;
    }
  }

}
