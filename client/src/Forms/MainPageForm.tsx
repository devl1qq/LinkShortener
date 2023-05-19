import React from "react";
import { Typography, Button, Container } from "@mui/material";
import { Link } from "react-router-dom";

const MainPageForm: React.FC = () => {
  return (
    <Container maxWidth="sm" sx={{ mt: 4 }}>
      <Typography variant="h5" align="center" sx={{ mb: 2 }}>
        LinkShortener
      </Typography>
      <Button
        variant="contained"
        color="primary"
        component={Link}
        to="/register"
        fullWidth
        sx={{ mb: 2 }}
      >
        Create Account
      </Button>
      <Button
        variant="contained"
        color="secondary"
        component={Link}
        to="/login"
        fullWidth
      >
        Enter Existing Account
      </Button>
    </Container>
  );
};

export default MainPageForm;
