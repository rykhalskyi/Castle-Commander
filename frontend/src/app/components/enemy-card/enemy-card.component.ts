import { Component, Input, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import { GameFlowService, IGameFlowState } from '../../services/game-flow.service';
import { CurrentCards } from '../../api-client';

@Component({
  selector: 'app-enemy-card',
  templateUrl: './enemy-card.component.html',
  styleUrl: './enemy-card.component.scss',
  standalone: true,
  imports: [NgIf]
})
export class EnemyCardComponent implements OnInit {
  @Input() currentCards: CurrentCards | null = null; 
  revealed = false;


  constructor(private readonly gameFlowService:GameFlowService){}
  
  ngOnInit(): void {
        
    }

  revealCard() {
    this.revealed = true;
    
    const state = this.gameFlowService.defaultState();
    const value = this.currentCards?.impactCard?.value ?? 0;
    state.highlighHexColor = value < 1 ? '#f00' : '#0f0';
    state.highlightHexagons = true;

    this.gameFlowService.setGameFlow(state);
  }
}
