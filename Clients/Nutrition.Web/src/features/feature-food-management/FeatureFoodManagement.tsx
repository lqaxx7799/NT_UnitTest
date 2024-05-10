import { useQuery } from "@tanstack/react-query";
import { Button, Table, TableBody, TableCell, TableHeader, TableHeaderCell, TableRow, Title1, makeStyles } from "@fluentui/react-components";
import FoodService from "../../core/services/food.service";
import { Link, NavLink } from "react-router-dom";
import { useGlobalClasses } from "../../core/styles/global.style";

const useStyles = makeStyles({
  
});

const FeatureFoodManagement = () => {
  const {
    data,
    error,
    isPending: loading,
  } = useQuery({
    queryKey: ['foods'],
    queryFn: () => FoodService.listFoods()
  });

  const classes = useStyles();
  const globalClasses = useGlobalClasses();

  if (loading) {
    return (
      <div>Loading...</div>
    );
  };

  if (error) {
    return (
      <div>Error...</div>
    );
  }

  return (
    <div>
      <div className={globalClasses.pageHeader}>
        <Title1>Food</Title1>
      </div>
      <div className={globalClasses.pageActionGroup}>
        <Link to="/food/create">
          <Button>
            Create new
          </Button>
        </Link>
      </div>
      <Table arial-label="Food table">
        <TableHeader>
          <TableRow>
            <TableHeaderCell>Name</TableHeaderCell>
            <TableHeaderCell>Action</TableHeaderCell>
          </TableRow>
        </TableHeader>
        <TableBody>
          {data.map((item) => (
            <TableRow key={item.id}>
              <TableCell>
                <NavLink to={`/food/${item.id}`}>
                  {item.name}
                </NavLink>
              </TableCell>
              <TableCell>
                <Button>Edit</Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
};

export default FeatureFoodManagement;
