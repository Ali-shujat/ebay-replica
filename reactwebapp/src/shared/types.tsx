export type Action =
  | { type: "setEmail"; payload: string }
  | { type: "setPassword"; payload: string }
  | { type: "setIsButtonDisabled"; payload: boolean }
  | { type: "loginSuccess"; payload: string }
  | { type: "loginFailed"; payload: string }
  | { type: "setIsError"; payload: boolean };

export type RegisterAction =
  | { type: "setEmail"; payload: string }
  | { type: "setPassword"; payload: string }
  | { type: "setConfirmPassword"; payload: string };
