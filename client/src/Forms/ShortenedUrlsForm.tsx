import React, { useEffect, useState } from "react";
import axios from "axios";
import { Container, Typography, List, ListItem, Button, TextField, Box, Link } from "@mui/material";
import { useNavigate } from "react-router-dom";

interface ShortenedUrl {
  id: number;
  shortUrl: string;
  originalUrl: string;
  createDate: string;
  createdBy: string;
}

const ShortenedUrlsForm: React.FC = () => {
  const [urls, setUrls] = useState<ShortenedUrl[]>([]);
  const [originalUrl, setOriginalUrl] = useState("");
  const [error] = useState("");
  const [isAdmin, setIsAdmin] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    fetchUrls();
    checkUserRole();
  }, []);

  const fetchUrls = async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await axios.get<ShortenedUrl[]>("https://localhost:7215/api/Url/get_all_links", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      setUrls(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const checkUserRole = async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await axios.get("https://localhost:7215/api/Url/admin_check", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      setIsAdmin(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleMoreInfoClick = (id: number) => {
    navigate(`/details/${id}`);
  };

  const handleCreateShortLink = async (originalUrl: string) => {
    const token = localStorage.getItem("token");
    try {
      const response = await axios.post(
        "https://localhost:7215/api/Url/create_short_url",
        { url: originalUrl }, // Send the request payload with 'url' property
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
            Accept: "application/json",
          },
        }
      );

      const shortUrl = response.data;
      window.location.reload();
      console.log("Short URL:", shortUrl);

      // Perform any further actions with the short URL if needed
    } catch (error) {
      console.error("Error creating short URL:", error);
      // Handle error
    }
  };

  const handleDeleteLink = async (id: number) => {
    const token = localStorage.getItem("token");
    try {
      await axios.delete(`https://localhost:7215/api/Url/delete_link/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      fetchUrls(); // Refresh the links after deletion
    } catch (error) {
      console.error("Error deleting link:", error);
      // Handle error
    }
  };

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Typography variant="h5" align="center" sx={{ mb: 2 }}>
        Shortened URLs
      </Typography>
      <TextField
        label="Original URL"
        value={originalUrl}
        onChange={(e) => setOriginalUrl(e.target.value)}
        variant="outlined"
        fullWidth
        sx={{ mb: 2 }}
      />
      <Button variant="contained" color="primary" onClick={() => handleCreateShortLink(originalUrl)}>
        Create Short Link
      </Button>
      {error && (
        <Typography variant="body1" align="center" color="error" sx={{ mt: 2 }}>
          {error}
        </Typography>
      )}
      <List sx={{ mt: 2 }}>
        {urls.map((url) => (
          <ListItem key={url.id}>
            <Box sx={{ display: "flex", alignItems: "center" }}>
              <Typography variant="body1" sx={{ marginRight: "8px" }}>
                Short Link:
              </Typography>
              <Link href={url.shortUrl} target="_blank" rel="noopener noreferrer">
                {url.shortUrl}
              </Link>
            </Box>
            <Box sx={{ marginLeft: "auto" }}>
              <Button variant="contained" color="primary" onClick={() => handleMoreInfoClick(url.id)}>
                More Information
              </Button>
              {isAdmin && (
                <Button variant="contained" color="error" onClick={() => handleDeleteLink(url.id)}>
                  Delete
                </Button>
              )}
            </Box>
          </ListItem>
        ))}
      </List>
    </Container>
  );
};

export default ShortenedUrlsForm;
