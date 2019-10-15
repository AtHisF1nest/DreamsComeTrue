export interface User {
    id?: number;
    login?: string;
    name?: string;
    photoUrl?: string;
    isInvited?: boolean;
    invitedYou?: boolean;
    avatar?: File;
    hasPair?: boolean;
}
