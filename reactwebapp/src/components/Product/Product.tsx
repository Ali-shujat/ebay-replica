import {
  Button,
  Card,
  CardActionArea,
  CardActions,
  CardContent,
  CardMedia,
  Typography,
} from "@mui/material";
import { IProduct } from "../../shared/Interface";
import "./Product.css";

const Product = (prop: IProduct) => {
  return (
    <>
      <Card sx={{ maxWidth: 345, maxHeight: 500 }}>
        <CardActionArea>
          <CardMedia component="img" height="140" image={prop.imageUrl} />
          <CardContent>
            <Typography
              gutterBottom
              variant="h5"
              component="div"
              sx={{
                overflow: "hidden",
                textOverflow: "ellipsis",
                display: "-webkit-box",
                WebkitLineClamp: "2",
                WebkitBoxOrient: "vertical",
              }}
            >
              {prop.title}
            </Typography>
            <Typography
              sx={{
                overflow: "hidden",
                textOverflow: "ellipsis",
                display: "-webkit-box",
                WebkitLineClamp: "3",
                WebkitBoxOrient: "vertical",
              }}
            >
              {prop.description}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              {prop.price}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              {prop.quantity} | {prop.storeId} | {prop.category}
            </Typography>
          </CardContent>
        </CardActionArea>
        <CardActions>
          <Button size="medium" color="primary">
            Add to Basket
          </Button>
        </CardActions>
      </Card>
    </>
  );
};

export default Product;
