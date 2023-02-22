import React from "react";
import { Navigate, PathRouteProps, Route, RouteProps } from "react-router-dom";

// interface RouteProps {
//     caseSensitive?: boolean;
//     children?: React.ReactNode;
//     element?: React.ReactElement | null;
//     index?: boolean;
//     path?: string;
//   }
// export interface Props extends RouteProps{
//     isAuth?: boolean
//   }

export interface Props extends PathRouteProps {
  isAuth?: boolean;
}

const ProtectedRoute = ({ isAuth, ...routeProps }: Props) => {
  if (isAuth) {
    return <Route {...routeProps} />;
  }
  return <Navigate to="/Login" replace />;
};

export default ProtectedRoute;
