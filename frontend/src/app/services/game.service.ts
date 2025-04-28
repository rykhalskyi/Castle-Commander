import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable, take } from 'rxjs';
import { IGame } from '../dto/interfaces';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http: HttpClient) { } 

  startNewGame(): Observable<any> {
    const url = `${environment.apiUrl}/api/game/startnew`;
    return this.http.get<IGame>(url).pipe(take(1));
  }
}
