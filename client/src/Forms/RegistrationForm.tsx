import React, { useState } from "react";
import { Typography, TextField, Button, Container } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";

const RegistrationForm: React.FC = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "https://localhost:7215/api/User/register",
        {
          username,
          password
        }
      );

      if (response.status === 200) {
        navigate("/login");
      } else {
        setError("Something went wrong");
      }
    } catch (error) {
      setError("Something went wrong");
      console.error(error);
    }
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 4 }}>
      <Typography variant="h5" align="center" sx={{ mb: 2 }}>
        Registration Form
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Username"
          fullWidth
          margin="normal"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <TextField
          label="Password"
          fullWidth
          margin="normal"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <Button variant="contained" color="primary" type="submit" fullWidth>
          Register
        </Button>
      </form>
      {error && (
        <div className="error-popup">
          <Typography variant="body1" color="error" align="center">
            {error}
          </Typography>
        </div>
      )}
      <Button
        variant="outlined"
        color="primary"
        component={Link}
        to="/"
        fullWidth
        sx={{ mt: 2 }}
      >
        Return to Main Page
      </Button>
    </Container>
  );
};

export default RegistrationForm;
