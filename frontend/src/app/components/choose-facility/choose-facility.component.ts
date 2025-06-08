import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { GameService } from '../../services/game.service';
import { FacilitySize } from '../../api-client';

@Component({
  selector: 'app-choose-facility',
  imports: [CommonModule],
  templateUrl: './choose-facility.component.html',
  styleUrl: './choose-facility.component.scss'
})
export class ChooseFacilityComponent {

  selectedFacility: number = 0;

constructor (private readonly gameService: GameService) { }

  selectFacility(facilitySize: number): void {
   
    if (facilitySize !== 0)
    {
      this.gameService.selectedFacilitySize = facilitySize;    
    }
    
    this.selectedFacility = facilitySize
    this.gameService.rapair = facilitySize == 0;  
    console.log('select ', this.gameService.rapair, this.gameService.selectedFacilitySize) 
  }
}
