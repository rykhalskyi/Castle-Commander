import { ChangeDetectorRef, Component, Input, input, OnInit } from '@angular/core';
import { ExchangeItem, BuyItemInput, Player, PlayerResource } from '../../api-client';
import { CommonModule } from '@angular/common';
import { GameService } from '../../services/game.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'app-resource-exchange',
  imports: [CommonModule],
  templateUrl: './resource-exchange.component.html',
  styleUrl: './resource-exchange.component.scss'
})
export class ResourceExchangeComponent implements OnInit {

@Input() currentPlayer: number = 0;
@Input() gameId: string | null = null;
@Input() players: Player[] = [];

protected bronzePrice: string[] = [];
protected silverPrice: string[] = [];
protected goldPrice: string[] = [];

protected otherPlayers:Player[] = [];
protected player: Player | null = null; 
protected availiableResources: string[] = [];
protected allResources: string[] = [];

protected playerCanByOnMarket: boolean = false;

selectedResourceColor: string = '';
selectedOtherResourceColor: string = '';

selectedOtherPlayer: Player | null = null;
otherPlayerResources: string[] = [];

protected selectedBuyResourceColor: string = '';
protected selectedBuyOtherResourceColor: string = '';


constructor(private readonly gameService: GameService,
            private readonly cdr: ChangeDetectorRef
) { }

ngOnInit(): void {
  this.player = this.players[this.currentPlayer];
  const baseResources = this.player?.resources?.filter(r => r.isBase).map(i=>i.color!) ?? [];
  const otherResources = this.player?.resources?.filter(r => !r.isBase).map(i=>i.color!) ?? [];

  this.bronzePrice = baseResources;
  this.silverPrice = otherResources;
  this.goldPrice = baseResources.concat(otherResources);

  this.availiableResources = this.player?.resources
  ?.filter(i=>i.number! > 0)
  .map(i=>i.color ?? '#cccccc') ?? [];

  this.allResources = this.player?.resources?.map(i=>i.color ?? '#cccccc') ?? [];

  this.otherPlayers = this.players.filter((_, idx) => idx !== this.currentPlayer);

   this.otherPlayerResources = [...(this.otherPlayers[0].resources
    ?.filter(r => (r.number ?? 0) > 0).map(i => i.color ?? "#cccccc") || [])];

  this.selectedOtherPlayer = this.otherPlayers[0];

   this.gameService.activeGame
   .pipe(untilDestroyed(this))
   .subscribe((game) => {
      if (game) {
        const marketFacilities = game.castle!.hexagons![0].facilities?.filter(i=>i.playerId == game.currentPlayer)
        this.playerCanByOnMarket = marketFacilities !== undefined && marketFacilities.length > 0;
      }
    });
   
  //
}

 protected async buy(item: ExchangeItem): Promise<void> {
  if (!this.gameId) return;

   await this.gameService.buy(this.gameId ?? '', item);
 }

 protected async exchangeWithPlayer()
 {
    if (this.selectedResourceColor && this.selectedOtherResourceColor && this.selectedOtherPlayer)
    {
        const otherPlayer = this.players.findIndex(i => i.name === this.selectedOtherPlayer!.name);
        const resource = this.player?.resources?.findIndex(i=> i.color === this.selectedResourceColor);
        const otherPlayerResources = this.selectedOtherPlayer?.resources?.findIndex(i=> i.color === this.selectedOtherResourceColor);
        await this.gameService.exchange(this.gameId!, otherPlayer , resource!, otherPlayerResources!);
    }
      console.log('can exchange');
 }

 protected async buyOnMarket()
 {
    if (this.selectedBuyResourceColor && this.selectedBuyOtherResourceColor)
    {
        const resourceToSell = this.player?.resources?.findIndex(i=> i.color === this.selectedBuyResourceColor);
        const resourceToBuy = this.player?.resources?.findIndex(i=> i.color === this.selectedBuyOtherResourceColor);
        await this.gameService.buyOnMarket(this.gameId! , resourceToSell!, resourceToBuy!);
    }
 }

 protected onResourceChange(event: Event): void {
  const select = event.target as HTMLSelectElement;
  this.selectedResourceColor = select.value;
}

 protected onOtherResourceChange(event: Event): void {
  const select = event.target as HTMLSelectElement;
  this.selectedOtherResourceColor = select.value;
}

protected onOtherPlayerChange(event: Event): void {
  const select = event.target as HTMLSelectElement;
  const playerName = select.value;
  this.selectedOtherPlayer = this.otherPlayers.find(p => p.name === playerName) || null;
  this.otherPlayerResources = [...(this.selectedOtherPlayer?.resources
    ?.filter(r => (r.number ?? 0) > 0).map(i => i.color ?? "#cccccc") || [])];
}

protected onBuyResourceChange(event: Event): void {
  const select = event.target as HTMLSelectElement;
  this.selectedBuyResourceColor = select.value;
}

 protected onOtherBuyResourceChange(event: Event): void {
  const select = event.target as HTMLSelectElement;
  this.selectedBuyOtherResourceColor = select.value;
}
}


