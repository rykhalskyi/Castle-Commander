import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Hexagon } from '../../api-client';
import { GameService } from '../../services/game.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-small-facilities',
  imports: [CommonModule],
  templateUrl: './small-faclities.component.svg',
  styleUrl: './small-facilities.component.scss'
})
export class SmallFacilitiesComponent implements OnInit, OnChanges {

  @Input() model: Hexagon | null = null;
  protected visibility: boolean[] = [false, false, false, false, false, false];
  protected primaryColors: string[] = ['#FF0000', '#00FF00', '#0000FF', '#FFFF00', '#FF00FF', '#00FFFF'];
  protected secondaryColors: string[] = ['#FFAAAA', '#AAFFAA', '#AAAAFF', '#FFFFAA', '#FFAAFF', '#AAFFFF'];
  protected tower:boolean = false;
  protected towerPrimaryColor:string = '#FFAAAA';
  protected towerSecondaryColor:string = '#FFAAAA';
     
  ngOnInit(): void {
    this.setSmallFacilities();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.setSmallFacilities();
  }
  
  private setSmallFacilities(): void {
    this.visibility = [false, false, false, false, false, false];

    const smallFacilities = this.model?.facilities?.filter(f => f.size === 1) ?? [];

    this.tower = this.model?.tower !== undefined;
    this.towerPrimaryColor = this.model?.tower?.primaryColor ??  "#FF0000";
    this.towerSecondaryColor = this.model?.tower?.secondaryColor ?? "#FFAAAA";

    if (smallFacilities.length === 0) {
      return;
    }

    for (let i = 0; i < smallFacilities.length; i++) {
      this.visibility[smallFacilities[i].startSector!] = true;
      this.primaryColors[smallFacilities[i].startSector!] = smallFacilities[i].primaryColor ?? "#FF0000";
      this.secondaryColors[smallFacilities[i].startSector!] = smallFacilities[i].secondaryColor ?? "#FFAAAA";
    }

    
  }



}
