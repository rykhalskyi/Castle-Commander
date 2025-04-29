import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Client } from '../api-client';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  public client: Client = new Client(`${environment.apiUrl}`);

  constructor() { } 
 
}
