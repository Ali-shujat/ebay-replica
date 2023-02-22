import {
  Button,
  Card,
  CardActions,
  CardContent,
  CardHeader,
  TextField,
} from "@material-ui/core";
import * as React from "react";
import { useReducer } from "react";
import AuthService from "../services/AuthService";
import { IRegState } from "../shared/Interface";
import { RegisterAction } from "../shared/types";
import { UseStyles } from "../shared/UseStyles";

const initialState: IRegState = {
  email: "",
  password: "",
  confirmPassword: "",
  // role: "",
  // uniqueStoreId: 0,
  // isError: false,
};

const reducer = (state: IRegState, action: RegisterAction): IRegState => {
  switch (action.type) {
    case "setEmail":
      return {
        ...state,
        email: action.payload,
      };
    case "setPassword":
      return {
        ...state,
        password: action.payload,
      };
    case "setConfirmPassword":
      return {
        ...state,
        confirmPassword: action.payload,
      };
  }
};
function Register() {
  const classes = UseStyles();
  const [state, dispatch] = useReducer(reducer, initialState);

  const handleKeyPress = (event: React.KeyboardEvent) => {
    if (event.keyCode === 13 || event.which === 13) {
     handleRegister();
    }
  };

  const handleEmailChange: React.ChangeEventHandler<HTMLInputElement> = (
    event
  ) => {
    dispatch({
      type: "setEmail",
      payload: event.target.value,
    });
  };

  const handlePasswordChange: React.ChangeEventHandler<HTMLInputElement> = (
    event
  ) => {
    dispatch({
      type: "setPassword",
      payload: event.target.value,
    });
  };
  const handleConfirmPasswordChange: React.ChangeEventHandler<
    HTMLInputElement
  > = (event) => {
    dispatch({
      type: "setConfirmPassword",
      payload: event.target.value,
    });
    console.log("🤦‍♂️🤦‍♂️" + event.target.value);
  };
  const handleRegister = () => {
    AuthService.register(
      state.email,
      state.password,
      state.confirmPassword
    ).then(
      (r) => {
       console.log("😎😎"+r)
      },
      (error) => {
        const resMessage =
          (error.response &&
            error.response.data &&
            error.response.data.message) ||
          error.message ||
          error.toString();
          console.log("😒😒"+resMessage)
      }
    );
  };
  return (
    <div>
      <form className={classes.container} noValidate autoComplete="off">
        <Card className={classes.card}>
          <CardHeader className={classes.header} title="Login App" />
          <CardContent>
            <div>
              <TextField
                fullWidth
                id="email"
                type="email"
                label="email"
                placeholder="email"
                margin="normal"
                onChange={handleEmailChange}
                onKeyPress={handleKeyPress}
              />
              <TextField
                fullWidth
                id="password"
                type="password"
                label="Password"
                placeholder="Password"
                margin="normal"
                onChange={handlePasswordChange}
                onKeyPress={handleKeyPress}
              />
              <TextField
                fullWidth
                id="ConfirmPassword"
                type="Password"
                label="ConfirmPassword"
                placeholder="ConfirmPassword"
                margin="normal"
                onChange={handleConfirmPasswordChange}
                onKeyPress={handleKeyPress}
              />
            </div>
          </CardContent>
          <CardActions>
            <Button
              variant="contained"
              size="large"
              color="secondary"
              className={classes.loginBtn}
              onClick={handleRegister}
              // disabled={state.isButtonDisabled}
            >
              Register
            </Button>
          </CardActions>
        </Card>
      </form>
    </div>
  );
}

export default Register;
