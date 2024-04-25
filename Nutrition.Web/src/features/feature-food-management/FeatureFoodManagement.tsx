import { useQuery } from "@tanstack/react-query";
import { Button, Table, TableBody, TableCell, TableHeader, TableHeaderCell, TableRow } from "@fluentui/react-components";
import FoodService from "../../core/services/food.service";

const FeatureFoodManagement = () => {
  const {
    data,
    error,
    isPending: loading,
  } = useQuery({
    queryKey: ['foods'],
    queryFn: () => FoodService.listFoods()
  });

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
      <div>Foods</div>
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
                {item.name}
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
