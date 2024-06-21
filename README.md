# Runid.FileGenerator

**Runid.FileGenerator** is a C# console application designed to generate multiple XML files based on a predefined template and customizable setup rules. This tool supports dynamic data generation using random values and data from external files, making it ideal for testing middleware, APIs, or other systems that require large sets of XML input data.

## Key Features
- **Template-Based Generation**: Define your file structure using a template with placeholders for dynamic data.
- **Customizable Rules**: Use a JSON setup file to specify rules for data generation, including random integers, dates, numbers, text, and data from external files.
- **Filename Convention**: Customize the naming convention for generated files, including date, time, and sequence placeholders.
- **Random Data Generation**: Generate random delivery numbers, quantities, batch numbers, and more.
- **Data File Integration**: Seamlessly integrate data from external files, ensuring consistency in related fields.

## Use Cases
- Testing and validating middleware and APIs with large sets of input data.
- Simulating real-world scenarios by generating diverse XML datasets.
- Automating the creation of XML files for batch processing systems.

## How to Use
1. **Define your template**: Create an XML template file with placeholders for dynamic data.
2. **Set up rules**: Create a setup JSON file to specify data generation rules.
3. **Generate files**: Run the application to generate XML files based on the template and rules.
