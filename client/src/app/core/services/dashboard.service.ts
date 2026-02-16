import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AgentDTO, PagedResult, QueueDTO, StatsDTO } from '../models/dashboard.model';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5081/api';

  getStats(): Observable<{ isSuccess: boolean; value: StatsDTO }> {
    return this.http.get<{ isSuccess: boolean; value: StatsDTO }>(`${this.apiUrl}/stats`);
  }

  getAgents(page: number = 1, pageSize: number = 10, filter: string = ''): Observable<{ isSuccess: boolean; value: PagedResult<AgentDTO> }> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    if (filter) {
      params = params.set('filters', `Name@=${filter}`);
    }

    return this.http.get<{ isSuccess: boolean; value: PagedResult<AgentDTO> }>(`${this.apiUrl}/agents`, { params });
  }

  getQueues(page: number = 1, pageSize: number = 10): Observable<{ isSuccess: boolean; value: PagedResult<QueueDTO> }> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<{ isSuccess: boolean; value: PagedResult<QueueDTO> }>(`${this.apiUrl}/queues`, { params });
  }
}
