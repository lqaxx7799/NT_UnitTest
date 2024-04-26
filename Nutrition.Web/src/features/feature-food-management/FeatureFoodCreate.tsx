import { Button, Input, Label, Title1, makeStyles, useId, shorthands, Combobox, Option, tokens, SpinButton, Title3, Select } from "@fluentui/react-components";
import { useGlobalClasses } from "../../core/styles/global.style";
import { Link } from "react-router-dom";
import { Controller, SubmitHandler, useFieldArray, useForm } from "react-hook-form";
import { IFoodCreateRequest } from "../../core/models/food.model";
import { useRef } from "react";
import CategoryService from "../../core/services/category.service";
import { useQuery } from "@tanstack/react-query";
import { Dismiss12Regular } from '@fluentui/react-icons';
import { SystemUnitOptions } from "../../core/constants/system-unit.constant";

const useClasses = makeStyles({
  formGrid: {
    display: 'grid',
    ...shorthands.gap('16px'),
    gridTemplateColumns: 'repeat(2, 1fr)',
    marginBottom: '8px',
  },
  tagsList: {
    listStyleType: "none",
    marginBottom: tokens.spacingVerticalXXS,
    marginTop: '8px',
    paddingLeft: 0,
    display: "flex",
    gridGap: tokens.spacingHorizontalXXS,
  },
  foodVariationWrapper: {
    ...shorthands.padding('12px'),
    ...shorthands.borderRadius(tokens.borderRadiusSmall),
    boxShadow: tokens.shadow8,
    marginBottom: '24px',
  }
});

const FeatureFoodCreate = () => {
  // css classes
  const globalClasses = useGlobalClasses();
  const classes = useClasses();

  // form
  const {
    control,
    register,
    handleSubmit,
    watch,
    formState: { errors },
    setValue: setFormValue,
    getValues: getFormValues,
  } = useForm<IFoodCreateRequest>({
    defaultValues: {
      name: '',
      categoryIds: [],
    },
  });
  const foodVariationsFieldArray = useFieldArray({
    control, // control props comes from useForm (optional: if you are using FormProvider)
    name: 'foodVariations', // unique name for your Field Array
  });

  // ids, ref
  const comboboxInputRef = useRef<any>(null);
  const selectedCategoryListRef = useRef<HTMLUListElement>(null);
  const nameControlId = useId("input-name");
  const categoryIdsControlId = useId("input-categoryIds");

  // fetch data
  const categoriesQuery = useQuery({
    queryKey: ['categories'],
    queryFn: () => CategoryService.listCategories()
  });

  // event handlers
  const onCategoryTagClick = (option: string, index: number) => {
    // remove selected option
    setFormValue('categoryIds', getFormValues().categoryIds.filter((o) => o !== option));

    // focus previous or next option, defaulting to focusing back to the combo input
    const indexToFocus = index === 0 ? 1 : index - 1;
    const optionToFocus = selectedCategoryListRef.current?.querySelector(
      `#${categoryIdsControlId}-remove-${indexToFocus}`
    );
    if (optionToFocus) {
      (optionToFocus as HTMLButtonElement).focus();
    } else {
      comboboxInputRef.current?.focus();
    }
  };

  const addFoodVariation = () => {
    setFormValue('foodVariations', [...getFormValues().foodVariations, {
      variationDescription: '',
      caloriesPerServing: 0,
      nutritionServingAmount: 0,
      nutritionServingUnit: '',
      nutritions: []
    }]);
  };

  const removeFoodVariation = (index: number) => {
    setFormValue('foodVariations', getFormValues().foodVariations.filter((_, i) => i !== index));
  }

  const onSubmit: SubmitHandler<IFoodCreateRequest> = (data) => {
    console.log(1111, data);
  };

  return (
    <div>
      <div className={globalClasses.pageHeader}>
        <Title1>Create Food</Title1>
      </div>
      <div className={globalClasses.pageActionGroup}>
        <Link to="/food">
          <Button>
            Back
          </Button>
        </Link>
      </div>
      <div>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className={classes.formGrid}>
            <Controller
              name="name"
              control={control}
              render={({ field }) => (
                <div className={globalClasses.formControlGroup}>
                  <Label htmlFor={nameControlId}>
                    Name
                  </Label>
                  <Input {...field} id={nameControlId} placeholder="Insert name" />
                </div>
              )}
            />
            <Controller
              name="categoryIds"
              control={control}
              render={({ field }) => (
                <div className={globalClasses.formControlGroup}>
                  <Label htmlFor={categoryIdsControlId}>
                    Name
                  </Label>
                  <div>
                    <Combobox
                      selectedOptions={field.value}
                      onOptionSelect={(_, data) => field.onChange(data.selectedOptions)}
                      id={categoryIdsControlId}
                      multiselect={true}
                      placeholder="Select one or more categories"
                      ref={comboboxInputRef}
                    >
                      {!categoriesQuery.isPending && !categoriesQuery.error && (
                        <>
                          {categoriesQuery.data.map((category) => (
                            <Option key={category.id} value={category.id}>{category.name}</Option>
                          ))}
                        </>
                      )}                    
                    </Combobox>
                    {field.value.length ? (
                      <ul
                        id={`${categoryIdsControlId}-selection`}
                        className={classes.tagsList}
                        ref={selectedCategoryListRef}
                      >
                        {/* The "Remove" span is used for naming the buttons without affecting the Combobox name */}
                        <span id={`${categoryIdsControlId}-remove`} hidden>
                          Remove
                        </span>
                        {field.value.map((option, i) => (
                          <li key={option}>
                            <Button
                              size="small"
                              shape="circular"
                              appearance="primary"
                              icon={<Dismiss12Regular />}
                              iconPosition="after"
                              onClick={() => onCategoryTagClick(option, i)}
                              id={`${categoryIdsControlId}-remove-${i}`}
                              aria-labelledby={`${categoryIdsControlId}-remove ${categoryIdsControlId}-remove-${i}`}
                            >
                              {categoriesQuery.data?.find((item) => item.id === option)?.name}
                            </Button>
                          </li>
                        ))}
                      </ul>
                    ) : null}
                  </div>
                </div>
              )}
            />
          </div>

          <Title3>Variations</Title3>
          {
            foodVariationsFieldArray.fields.map((field, index) => (
              <div key={field.id} className={classes.foodVariationWrapper}>
                <div className={classes.formGrid}>
                  <Controller
                    name={`foodVariations.${index}.variationDescription`}
                    control={control}
                    render={({ field }) => (
                      <div className={globalClasses.formControlGroup}>
                        <Label htmlFor={`foodVariations.${index}.variationDescription`}>
                          Description
                        </Label>
                        <Input {...field} id={`foodVariations.${index}.variationDescription`} placeholder="Insert description" />
                      </div>
                    )}
                  />
                  <Controller
                    name={`foodVariations.${index}.caloriesPerServing`}
                    control={control}
                    render={({ field }) => (
                      <div className={globalClasses.formControlGroup}>
                        <Label htmlFor={`foodVariations.${index}.caloriesPerServing`}>
                          Calories per Serving
                        </Label>
                        <SpinButton {...field} id={`foodVariations.${index}.caloriesPerServing`} placeholder="Insert calories per serving" step={1} />
                      </div>
                    )}
                  />
                </div>
                <div className={classes.formGrid}>
                  <Controller
                    name={`foodVariations.${index}.nutritionServingAmount`}
                    control={control}
                    render={({ field }) => (
                      <div className={globalClasses.formControlGroup}>
                        <Label htmlFor={`foodVariations.${index}.nutritionServingAmount`}>
                          Nutrition serving amount
                        </Label>
                        <SpinButton {...field} id={`foodVariations.${index}.nutritionServingAmount`} placeholder="Insert nutrition serving amount" step={0.01} />
                      </div>
                    )}
                  />
                  <Controller
                    name={`foodVariations.${index}.nutritionServingUnit`}
                    control={control}
                    render={({ field }) => (
                      <div className={globalClasses.formControlGroup}>
                        <Label htmlFor={`foodVariations.${index}.nutritionServingUnit`}>
                          Nutrition serving unit
                        </Label>
                        <Select {...field} id={`foodVariations.${index}.nutritionServingUnit`}>
                          {
                            SystemUnitOptions.map((option) => (
                              <option value={option.value} key={option.value}>{option.label}</option>
                            ))
                          }
                        </Select>
                      </div>
                    )}
                  />
                </div>
                <div>
                  <Button shape="circular" onClick={() => removeFoodVariation(index)}>Remove</Button>
                </div>
              </div>
            ))
          }
          <div className={classes.formGrid}>

          </div>
          <div>
            <Button onClick={addFoodVariation}>Add variation</Button>
          </div>
          
          <Button type="submit">Submit</Button>
        </form>
      </div>
    </div>
  );
};

export default FeatureFoodCreate;
