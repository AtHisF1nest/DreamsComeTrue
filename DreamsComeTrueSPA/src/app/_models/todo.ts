import { User } from './user';

export interface Todo {
    id: number;
    objective: string;
    cost: string;
    author: User;
    created: string;
    lastDone: string;
}
