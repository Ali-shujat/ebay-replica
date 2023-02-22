import { AppBar, Box, IconButton, Switch, Toolbar } from "@material-ui/core";
import { Home } from "@mui/icons-material";
import MenuIcon from "@mui/icons-material/Menu";
import * as React from "react";
import { Link, Route, Routes } from "react-router-dom";
import Login from "../components/Login";
import ProductPage from "../components/Product/ProductPage";
import Register from "../components/Register";
import IUser from "./Interface";
import "./NavBar.css";
import ProtectedRoute from "./ProtectedRoute";

type Stat = {
  showSuperAdminBoard: boolean;
  showAdminBoard: boolean;
  currentUser: IUser | undefined;
};

function Navbar() {
  const [isAuth, setIsAuth] = React.useState(false);
  const [stat, setStat] = React.useState<Stat>({
    showSuperAdminBoard: false,
    showAdminBoard: false,
    currentUser: undefined,
  });

  return (
    <>
      <Box sx={{ flexGrow: 1 }}>
        <AppBar position="static">
          <Toolbar>
            <IconButton
              size="medium"
              edge="start"
              color="secondary"
              aria-label="menu"
            >
              <MenuIcon />
            </IconButton>
            <Link to={"/ProductPage"} className="nav-link">
              Products
            </Link>
            <Link to={"/login"} className="nav-link">
              Login
            </Link>
            <Link to={"/register"} className="nav-link">
              Sign Up
            </Link>
          </Toolbar>
        </AppBar>
      </Box>
      <div>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/ProductPage" element={<ProductPage />} />
          {/* <Route path="/login" element={<Login />} /> */}

          <Route path="/login" element={<Login setIsAuth={setIsAuth} />} />
          {/* <ProtectedRoute
            isAuth={isAuth}
            path="/ProductPage"
            element={<ProductPage />}
          /> */}

          <Route path="/register" element={<Register />} />
        </Routes>
      </div>
    </>
  );
}

export default Navbar;
