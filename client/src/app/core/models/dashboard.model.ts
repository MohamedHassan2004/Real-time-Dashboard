export enum AgentStatus {
  Online = 0,
  Idle = 1,
  NotReady = 2
}

export interface StatsDTO {
  slaPercentage: number;
  totalOffered: number;
  answered: number;
  abandoned: number;
  inQueue: number;
}

export interface AgentDTO {
  name: string;
  status: AgentStatus;
  duration?: string;
  lastStatusChange: string;
}

export interface QueueDTO {
  name: string;
  inQueue: number;
  maxWait: string;
  oldestCallCreatedAt?: string;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}
