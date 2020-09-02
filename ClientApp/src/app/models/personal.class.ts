   export interface Person  {
    PersonID: number;
  
    /**  */
    FirstName: string;
  
    /**  */
    LastName: string;
  
    /**  */
    Birthdate?: Date;
  
    /**  */
    Gender?: Gender;
  
    /**  */
    EmailPrivate?: string;
  
    /**  */
    EmailAssociaton?: string;
  
    /**  */
    MobilePrivate?: string;
  
    /**  */
    AdressStreet?: string;
  
    /**  */
    AdressNr?: string;
  
    /**  */
    AdressZIP?: string;
  
    /**  */
    AdressCity?: string;
  
    /**  */
    PersonsMemberStatus: PersonsMemberStatus[];
  
    /**  */
    PersonsCareerLevels: PersonsCareerLevel[];
  
    /**  */
    PersonsPositions: PersonsPosition[];
  }
  
  export enum Gender {
    MALE = 'männlich',
  
    FEMALE = 'weiblich',
  
    DIVERSE = 'divers',
  }
  
  export interface PersonsMemberStatus  {
    PersonsMemberStatusID: number;
  
    PersonID: number;
  
    MemberStatusID: number;
  
    /**  */
    Begin: Date;
  
    /**  */
    End?: Date;
  
    Person?: Person;
  
    MemberStatus: MemberStatus;
  }
  
  export interface MemberStatus {
    MemberStatusID: number;
  
    /**  */
    Name: string;
  
    PersonsMemberStatus?: PersonsMemberStatus[];
  }
  
  interface PersonsCareerLevel  {
    PersonsCareerLevelID: number;
  
    PersonID: number;
  
    CareerLevelID: number;
  
    /**  */
    Begin: Date;
  
    /**  optional*/
    End?: Date;
  
    /**  optional*/
    Person?: Person;
  
    /**  */
    CareerLevel: CareerLevel;
  }
  
  export interface CareerLevel  {
    CareerLevelID: number;
  
    /**  */
    Name: string;
  
    /**  */
    ShortName: string;
  
    /**  */
    IsActive: boolean;
  
    PersonsCareerLevels: PersonsCareerLevel[];
  }
  
  export interface PersonsPosition  {
    PersonPositionID: number;
  
    PersonID: number;
  
    PositionID: number;
  
    /**  */
    Begin: Date;
  
    /**  optional */
    End?: Date;
  
    /**  optional */
    Person?: Person;
  
    Position: Position;
  }