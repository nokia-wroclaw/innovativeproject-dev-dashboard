import { Todo } from './todo';

export interface TodoResponse {
    documentation : String;
    swaggerEditor : String;
    toDoItems: Todo[];
}
