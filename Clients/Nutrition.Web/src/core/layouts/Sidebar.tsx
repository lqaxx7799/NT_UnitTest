import { CompoundButton, makeStyles, shorthands, tokens } from "@fluentui/react-components";
import { FoodAppleRegular, FoodRegular } from '@fluentui/react-icons';
import { NavLink } from "react-router-dom";

const useStyles = makeStyles({
  sidebarLogo: {
    fontSize: '18px',
    fontWeight: 'bold',
    ...shorthands.margin('12px', '8px', '24px', '8px'),
  },
  sidebarWrapper: {
    width: '300px',
    boxShadow: tokens.shadow16,
    ...shorthands.padding('12px'),
  },
  sidebarItem: {
    width: '100%',
    justifyContent: 'start',
    paddingTop: '8px',
    paddingBottom: '8px',
    marginBottom: '12px',
  },
});

const sidebarItems = [
  {
    title: 'Food Management',
    icon: <FoodAppleRegular />,
    route: 'food',
  },
  {
    title: 'Meal Management',
    icon: <FoodRegular />,
    route: 'meal',
  },
];

const Sidebar = () => {
  const classes = useStyles();
  return (
    <div className={classes.sidebarWrapper}>
      <div className={classes.sidebarLogo}>Nutrition System</div>
      {
        sidebarItems.map((item) => (
          <NavLink to={item.route} key={item.route}>
            {({ isActive }) => (
              <CompoundButton
                className={classes.sidebarItem}
                key={item.title}
                icon={item.icon}
                appearance={isActive ? 'primary' : undefined}
              >
                {item.title}
              </CompoundButton>
            )}
          </NavLink>
        ))
      }
    </div>
  );
};

export default Sidebar;
