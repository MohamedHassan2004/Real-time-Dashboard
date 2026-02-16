import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { AgentDTO, QueueDTO, StatsDTO } from '../models/dashboard.model';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: HubConnection;
  private hubUrl = 'http://localhost:5081/hubs/dashboard';

  private statsSubject = new BehaviorSubject<StatsDTO | null>(null);
  public stats$ = this.statsSubject.asObservable();

  private agentsSubject = new BehaviorSubject<AgentDTO[]>([]);
  public agents$ = this.agentsSubject.asObservable();

  private queuesSubject = new BehaviorSubject<QueueDTO[]>([]);
  public queues$ = this.queuesSubject.asObservable();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl)
      .withAutomaticReconnect()
      .build();

    this.startConnection();
    this.registerHandlers();
  }

  private startConnection() {
    if (this.hubConnection.state === HubConnectionState.Connected) {
      return;
    }

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection Started'))
      .catch(err => {
        console.error('Error while starting SignalR connection: ' + err);
        setTimeout(() => this.startConnection(), 5000);
      });
  }

  private registerHandlers() {
    this.hubConnection.on('ReceiveStats', (stats: StatsDTO) => {
      this.statsSubject.next(stats);
    });

    this.hubConnection.on('ReceiveAgents', (agents: AgentDTO[]) => {
      this.agentsSubject.next(agents);
    });

    this.hubConnection.on('ReceiveQueues', (queues: QueueDTO[]) => {
      this.queuesSubject.next(queues);
    });
  }
}
