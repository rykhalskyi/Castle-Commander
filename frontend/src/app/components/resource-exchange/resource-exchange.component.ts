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

@Input() currentPlayerId: string = '';
@Input() gameId: string | null = null;
@Input() players: Player[] = [];
@Input() buttonsDisabled: boolean = true;

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

protected bronzeOnMarket: string[] = ["","",""];
protected silberOnMarket: string[] = ["","",""];

constructor(private readonly gameService: GameService,
            private readonly cdr: ChangeDetectorRef
) { }

ngOnInit(): void {
  this.player = this.players.find(i=> i.id === this.currentPlayerId) ?? null;

  const baseResources = this.player?.resources?.filter(r => r.isBase).map(i=>i.color!) ?? [];
  const otherResources = this.player?.resources?.filter(r => !r.isBase).map(i=>i.color!) ?? [];

  this.bronzePrice = baseResources;
  this.silverPrice = otherResources;
  this.goldPrice = baseResources.concat(otherResources);

  this.availiableResources = this.player?.resources
  ?.filter(i=>i.number! > 0)
  .map(i=>i.color ?? '#cccccc') ?? [];

  this.allResources = this.player?.resources?.map(i=>i.color ?? '#cccccc') ?? [];
  this.bronzeOnMarket = this.allResources.slice(0,3);
  this.silberOnMarket = this.allResources.slice(3,6);
 
  //it can be no other players
  this.otherPlayers = this.players.filter(i=> i.id !== this.currentPlayerId);
  const onlyOnePlayer = this.otherPlayers.length === 0;
  
  if (!onlyOnePlayer)
  {
    this.otherPlayerResources = [...(this.otherPlayers[0].resources
      ?.filter(r => (r.number ?? 0) > 0).map(i => i.color ?? "#cccccc") || [])];

    this.selectedOtherPlayer = this.otherPlayers[0];
  }
  
  this.gameService.activeGame
   .pipe(untilDestroyed(this))
   .subscribe((game) => {
      if (game) {
        const marketFacilities = game.castle!.hexagons![0].facilities?.filter(i=>i.playerId == game.currentPlayer)
        this.playerCanByOnMarket = marketFacilities !== undefined && marketFacilities.length > 0;
      }
    });
}

 protected async buy(item: ExchangeItem): Promise<void> {
  if (!this.gameId || this.buttonsDisabled) return;

   await this.gameService.buy(this.gameId ?? '', this.player!.id!, item);
 }

 protected async exchangeWithPlayer()
 {
    if (this.buttonsDisabled) return;
    if (this.selectedResourceColor && this.selectedOtherResourceColor && this.selectedOtherPlayer)
    {
        const resource = this.player?.resources?.findIndex(i=> i.color === this.selectedResourceColor);
        const otherPlayerResources = this.selectedOtherPlayer?.resources?.findIndex(i=> i.color === this.selectedOtherResourceColor);
        await this.gameService.exchange(this.gameId!, this.player!.id!, this.selectedOtherPlayer!.id! , resource!, otherPlayerResources!);
    }
      console.log('can exchange');
 }

 protected async buyOnMarket()
 {
    if (this.buttonsDisabled) return;
    if (this.selectedBuyResourceColor && this.selectedBuyOtherResourceColor)
    {
        const resourceToSell = this.player?.resources?.findIndex(i=> i.color === this.selectedBuyResourceColor);
        const resourceToBuy = this.player?.resources?.findIndex(i=> i.color === this.selectedBuyOtherResourceColor);
        await this.gameService.buyOnMarket(this.gameId!, this.player!.id!, resourceToSell!, resourceToBuy!);
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

protected  onResourceArrayChanged(event: Event, array:string[], index: number)
{
  const select = event.target as HTMLSelectElement;
  array[index] = select.value;
}

protected async buyCoinOnMarket(item:ExchangeItem):Promise<void>
{
    if (this.buttonsDisabled) return;
    let resources: number[] = [0,0,0];
    if (item === 0 || item === 1) {
      const marketArray = item === 0 ? this.bronzeOnMarket : this.silberOnMarket;
      for (let idx = 0; idx < 3; idx++) {
        resources[idx] = this.player?.resources?.findIndex(i => i.color === marketArray[idx]) ?? 0;
      }
    }

    await this.gameService.buyCoinOnMarket(this.gameId!, item, resources);
}

}


