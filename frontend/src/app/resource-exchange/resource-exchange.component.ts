import { Component, Input, input, OnInit } from '@angular/core';
import { ExchangeItem, ExchangeItemInput, Player } from '../api-client';
import { CommonModule } from '@angular/common';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-resource-exchange',
  imports: [CommonModule],
  templateUrl: './resource-exchange.component.html',
  styleUrl: './resource-exchange.component.scss'
})
export class ResourceExchangeComponent implements OnInit {

@Input() player: Player | null = null;
@Input() gameId: string | null = null;

protected bronzePrice: string[] = [];
protected silverPrice: string[] = [];
protected goldPrice: string[] = [];

constructor(private readonly gameService: GameService) { }

ngOnInit(): void {
  const baseResources = this.player?.resources?.filter(r => r.isBase).map(i=>i.color!) ?? [];
  const otherResources = this.player?.resources?.filter(r => !r.isBase).map(i=>i.color!) ?? [];

  this.bronzePrice = baseResources;
  this.silverPrice = otherResources;
  this.goldPrice = baseResources.concat(otherResources);
}
 protected async exchange(item: ExchangeItem): Promise<void> {
  console.log('exchange', this.gameId, item);
  if (!this.gameId) return;

   await this.gameService.exchange(this.gameId ?? '', item);
 }

}
