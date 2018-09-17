import { User } from './user';

export interface Todo {
    id: number;
    objective: string;
    cost: number;
    author: User;
    created: string;
    lastDone: string;
}
