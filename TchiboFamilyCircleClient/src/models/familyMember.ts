import { Occasion } from './occasion';

export interface FamilyMember {
    id: string;
    name: string;
    type: string;
    dateOfBirth: string;
    occasions: Occasion[];
    sizes: string[];
    interests: string[];
    budget: number | null;
    customerNumber: string;        
  }