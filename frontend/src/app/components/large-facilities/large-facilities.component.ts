import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { Hexagon } from '../../api-client';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-large-facilities',
  imports: [CommonModule],
  templateUrl: './large-facilities.component.svg',
  styleUrl: './large-facilities.component.scss'
})
export class LargeFacilitiesComponent implements OnInit, OnChanges {
  @Input() model: Hexagon | null = null;
  protected visibility: boolean[] = [false, false, false, false, false, false];
  protected primaryColors: string[] = ['#FF0000', '#00FF00', '#0000FF', '#FFFF00', '#FF00FF', '#00FFFF'];
  protected secondaryColors: string[] = ['#FFAAAA', '#AAFFAA', '#AAAAFF', '#FFFFAA', '#FFAAFF', '#AAFFFF'];

  ngOnInit(): void {
    this.setLargeFacilities();
  }

  ngOnChanges(): void {
    this.setLargeFacilities();
  }

  private setLargeFacilities(): void {
    this.visibility = [false, false, false, false, false, false];

    const largeFacilities = this.model?.facilities?.filter(f => f.size === 3) ?? [];
    if (largeFacilities.length === 0) {
      return;
    }
    for (let i = 0; i < largeFacilities.length; i++) {
      this.visibility[largeFacilities[i].startSector!] = true;
      this.primaryColors[largeFacilities[i].startSector!] = largeFacilities[i].primaryColor ?? "#FF0000";
      this.secondaryColors[largeFacilities[i].startSector!] = largeFacilities[i].secondaryColor ?? "#FFAAAA";
    }
  }
}
