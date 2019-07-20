import { Photo } from './Photo';

export interface User {
    id: number;
    username: string;
    photos?: Photo[];
}
