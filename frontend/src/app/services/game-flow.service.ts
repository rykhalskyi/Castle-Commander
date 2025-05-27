import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export interface IGameFlowState {
  highlightHexagons: boolean;
  highlighHexColor: string;
}

@Injectable({
  providedIn: 'root'
})
export class GameFlowService {
  private gameFlowStateSubject = new BehaviorSubject<IGameFlowState>(this.defaultState());
  public readonly gameFlowState$: Observable<IGameFlowState> = this.gameFlowStateSubject.asObservable();

  public setGameFlow(newState: IGameFlowState): void {
    this.gameFlowStateSubject.next(newState);
  }

  public setDefaultState():void{
    this.gameFlowStateSubject.next(this.defaultState());
  }

  public defaultState():IGameFlowState
  {
    return {
        highlightHexagons: false,
        highlighHexColor: '#ccc'
    }
  }
}
