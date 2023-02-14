import {
  Box,
  AppBar,
  Toolbar,
  IconButton,
  Typography,
  Button,
} from "@material-ui/core";
import MenuIcon from "@mui/icons-material/Menu";
import * as React from "react";

function Navbar() {
  return (
    <>
      <Box sx={{ flexGrow: 1 }}>
        <AppBar position="static">
          <Toolbar>
            <IconButton
              size="medium"
              edge="start"
              color="default"
              aria-label="menu"
              
            >
              <MenuIcon />
            </IconButton>
            <Typography variant="h6" component="div">
              Home
            </Typography>
            <Button color="inherit">Login</Button>
          </Toolbar>
        </AppBar>
      </Box>
    </>
  );
}

export default Navbar;
