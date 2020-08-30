interface PersonBase {
  getDiplayName(property: string): string;
}

export interface Person extends PersonBase {
  PersonID: number;

  /** Display Name */
  FirstName: string;

  /** Display Name */
  LastName: string;

  /** Display Name */
  Birthdate?: Date;

  /** Display Name */
  Gender?: Gender;

  /** Display Name */
  EmailPrivate?: string;

  /** Display Name */
  EmailAssociaton?: string;

  /** Display Name */
  MobilePrivate?: string;

  /** Display Name */
  AdressStreet?: string;

  /** Display Name */
  AdressNr?: string;

  /** Display Name */
  AdressZIP?: string;

  /** Display Name */
  AdressCity?: string;

  /** Display Name */
  PersonsMemberStatus: PersonsMemberStatus[];

  /** Display Name */
  PersonsCareerLevels: PersonsCareerLevel[];

  /** Display Name */
  PersonsPositions: PersonsPosition[];
}

export enum Gender {
  MALE = 'männlich',

  FEMALE = 'weiblich',

  DIVERSE = 'divers',
}

export interface PersonsMemberStatus extends PersonBase {
  PersonsMemberStatusID: number;

  PersonID: number;

  MemberStatusID: number;

  /** Display Name */
  Begin: Date;

  /** Display Name */
  End?: Date;

  Person?: Person;

  MemberStatus: MemberStatus;
}

export interface MemberStatus {
  MemberStatusID: number;

  /** Display Name */
  Name: string;

  PersonsMemberStatus?: PersonsMemberStatus[];
}

interface PersonsCareerLevel extends PersonBase {
  PersonsCareerLevelID: number;

  PersonID: number;

  CareerLevelID: number;

  /** Display Name */
  Begin: Date;

  /** Display Name; optional*/
  End?: Date;

  Person: Person;

  /** Display Name */
  CareerLevel: CareerLevel;
}

export interface CareerLevel extends PersonBase {
  CareerLevelID: number;

  /** Display Name */
  Name: string;

  /** Display Name */
  ShortName: string;

  /** Display Name */
  IsActive: boolean;

  PersonsCareerLevels: PersonsCareerLevel[];
}

export interface PersonsPosition extends PersonBase {
  PersonPositionID: number;

  PersonID: number;

  PositionID: number;

  /** Display Name */
  Begin: Date;

  /** Display Name; optional */
  End: Date;

  Person: Person;

  Position: Position;
}
