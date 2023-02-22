import {
  Container,
  FormControl,
  Grid,
  InputBase,
  InputLabel,
  MenuItem,
  Paper,
  Select,
} from "@mui/material";
import axios from "axios";
import { SetStateAction, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { IProduct } from "../../shared/Interface";
import Product from "./Product";

function ProductPage() {
  const [product, setProduct] = useState<IProduct[]>([
    {
      id: 444,
      title: "string",
      description: "string",
      imageUrl: "string",
      storeId: 90,
      price: "string",
      quantity: 880,
      category: "none",
    },
  ]);

  //add search and sort
  const [selectedCategory, setSelectedCategory] = useState<string>("");
  const [search, setSearch]: [string, (search: string) => void] = useState("");

  useEffect(() => {
    axios
      .get("https://localhost:7068/api/Products")
      .then((response) => {
        setProduct((data) => {
          return response.data;
        });
      })
      .catch((error) => {
        if (error.response) {
          console.log(error.response.data);
        }
      });
  }, []);
  const navigate = useNavigate();
  let options = new Array("All");
  product.map((c) => {
    options.push(c.category as string);
  });
  const uniqueNames = Array.from(new Set(options));

  // Function to get filtered list
  function getFilteredList() {
    // Avoid filter when selectedCategory is null
    if (!selectedCategory || selectedCategory === "All") {
      return product;
    }
    return product.filter((item) => item.category === selectedCategory);
  }
  // Avoid duplicate function calls with useMemo
  var filteredList = useMemo(getFilteredList, [selectedCategory, product]);
  // Add search panel
  const handleChange = (e: { target: { value: string } }) => {
    setSearch(e.target.value);
  };
  const selectHandleChange = (event: {
    target: { value: SetStateAction<string> };
  }) => {
    setSelectedCategory(event.target.value);
  };

  return (
    <>
      <Container>
        <Paper sx={{ m: 1, maxWidth: 200, float: "right" }}>
          <InputBase
            style={{ fontSize: "large", borderRadius: "15px" }}
            type="search"
            placeholder="🔍 Search"
            onChange={handleChange}
          />
        </Paper>
        <FormControl sx={{ m: 1, minWidth: 120 }}>
          <InputLabel id="demo-simple-select-label">Category</InputLabel>
          <Select
            labelId="demo-simple-select-label"
            name="category-list"
            id="category-list"
            label="Category"
            onChange={selectHandleChange}
          >
            {uniqueNames.map((c) => (
              <MenuItem value={c}>{c}</MenuItem>
            ))}
          </Select>
        </FormControl>
      </Container>
      <Container sx={{ py: 4 }} maxWidth="md">
        {/* End hero unit */}
        <Grid container spacing={2}>
          {filteredList &&
            filteredList.length > 0 &&
            filteredList.map((item) => {
              if (
                search === "" ||
                item.title.toLowerCase().includes(search.toLowerCase())
              ) {
                return (
                  <Grid item key={item.id} xs={4} sm={4} md={4}>
                    <Product {...item} />
                  </Grid>
                );
              }
              return null;
            })}
        </Grid>
      </Container>
    </>
  );
}

export default ProductPage;
