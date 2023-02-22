export interface IState {
  email: string;
  password: string;
  isButtonDisabled: boolean;
  helperText: string;
  isError: boolean;
}
export interface IRegState {
  email: string;
  password: string;
  confirmPassword: string;
  // role: string;
  // uniqueStoreId: number;
  // isError: boolean;
}

export default interface IUser {
  id?: any | null;
  username?: string | null;
  email?: string;
  password?: string;
  roles?: string;
}

export interface IProduct {
  id?: number| null;
  title: string;
  description?: string;
  imageUrl?: string;
  price?: string;
  quantity?: number;
  category?: string;
  storeId?: number | null;
}
