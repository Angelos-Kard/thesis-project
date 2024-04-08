# Preamble
Contains commants about the basic parameters of the file

- `\documentclass[optional_args]{type_of_document}`: Type of document -> type_of_document = article, beamer(for presentation)
- `\usepackage[parameters]{package_name}`: Allows to use packages
  - geometry: allows us to fix the margins
  - amsmath
  - amssymb
  - graphicx: in order to embed *.jpeg and *.png files

---

# Latex Workshop: Shortcuts

- `Ctrl + LMB` (in PDF): Go to .tex file
- `Ctrl + Alt + j` (in .tex file): Go to page in PDF file

---

# Document Environment

- `\begin{docuemnt}`: Begin of document
- `\end{docuemnt}`: End of document

---

# Text Manipulation

## 1. Font Sizes

| Commands | Font Sizes 1 (Default) | Font Sizes 2 | Font Sizes 3 |
|---|---|---|---|
| Optional Parameter | 10pt | 11pt | 12pt |
| `\tiny` | 5pt | 6pt | 6pt |
| `\scriptsize` | 7pt | 8pt | 8pt |
| `\footnotesize` | 8pt | 9pt | 10pt |
| `\normalsize` | 10pt | 11pt | 12pt |
| `\large` | 12pt | 12pt | 14pt |
| `\Large` | 14pt | 14pt | 17pt |
| `\LARGE` | 17pt | 17pt | 20pt |
| `\huge` | 20pt | 20pt | 25pt |
| `\Huge` | 25pt | 25pt | 25pt |

- Change font for a few words: 
```tex 
Normal text {\Large LARGE [Only words in curly brackets change]} text
```
- Change font for all the following words: 
```tex
Normal text \Large LARGE text
```

Packages for font sizes: extsizes, anyfontsize

## 2. Font Styles

| Command | Description | Result |
|---|---|---|
| `\textbf{Text}` | Bold Text | **Text** |
| `\textit{Text}` | Italic Text | *Text* |
| `\underline{Text}` | Underlined Text<br>(Not suitable for multiple lines) | <u>Text</u> |
| `\uline{Text}` | Underline multiple<br>lines of text<br>Package: **ulem** | <u>Text</u> |
| `\uuline{Text}` | Double underlined text<br>Package: **ulem** | |
| `\uwave{Text}` | Wavy underlined text<br>Package: **ulem** | |
| `\emph{Text}` | Decides the way of emphasis<br>Not working well with ulem |

## 3. Font Families

| Command | Description | Result |
|---|---|---|
| `\textrm{Text}` | (Default) Roman | $\textrm{Text}$ | 
| `\textsf{Text}` | Sans serif | $\textsf{Text}$
| `\texttt{Text}` | Typewriter (monospaced) | $\texttt{Text}$ |
| `\mathbb{Text}` | AMS Blackboard Font (For Maths) | $\mathbb{TEXT}$ |


---

## 4. Text Justification/Alignment

| Command | Description |
|---|---|
| - | Fully Justified Text (Default) |
| `\begin{center}...\end{center}` | Center Justified Text |
| `\begin{flushleft}...\end{flushleft}` | Left Justified Text |
| `\begin{flushright}...\end{flushright}` | Right Justified Text |

### 4.1 Line Breaks in Alternate Alignment Modes
| Command | Description |
|---|---|
| `Text \\` | New Line === `\n` |
| `Text \\[\baselineskip]` | New line and skips line === `\n\n` |
| `Text \\[2\baselineskip]` | New line and skips 2 lines === `\n\n\n` |

### 4.2 Indentation
- New paragraph is created with an empty line<br>
E.g.:
```tex
Paragraph 1
            <!-- This empty line will not be visible -->
<!-- There is indentation at the start of the paragraph -->New paragraph 2
```
- To delete the indentation, we write at the start of the pragraph: `\noindent`
E.g.:
```tex
Paragraph 1
            <!-- This empty line will not be visible -->
\noindent New paragraph 2
```
- To set the length of the indentation, it is set in the Preamble or at the beginning of the docuemtn or (should be avoided) just before a paragraph like this: `\setlength{\parindent}{1cm}`
  - Units that you can use: in, cm, mm, pt, em (width of 'm'), ex (height of x)

---

# Math Manipulation

## 1. Math Modes
- Display Style: Math on the center, on display
  - Always on a new paragraph and always centered
  - Command: `\[ f(x) = (x+2)^2 - 9 \]` -> Better for one line of equations
  - Command 2: Align* Environment
    - &: States where the alignment should be
    - \\\\: Indicates a new line
    - *: Equations are not numbered. If it is skipped, each equation will have a number next to it. To disable the numbering for a specific equation, we add the `\nonumber` at the end of the equation, but before the new line `\\`
    - Example:<br> 
  ```tex
  \begin{align*}
  f(x) & = a_2 x^2 + a_1 x + a_0 \\
  & = x^2 + 4x - 5
  \end{align*}
  ```
- Inline Style
  - Command: `$f(x) = (x+2)^2 - 9$` -> $f(x) = (x+2)^2 - 9$
  - Command 2: `\( f(x) = (x+2)^2 - 9 \)`
- Force Display Style in inline mode: `\( \displaystyle f(x) = (x+2)^2 - 9 \)`
- Force inline math in display mode: `\[ \textstyle f(x) = (x+2)^2 - 9 \]`

## 2. Basic Notation
| Command | Description | Result |
|---|---|---|
| `+` | - | $1+1$ |
| `-` | - | $5-3$ |
| `\cdot` | Multiplication: Dot symbol | $6 \cdot 4$ |
| `\times` | Multiplication: X symbol | $6 \times 4$ |
| `\div` | Division: symbol | $27 \div 7$ |
| `\frac{numerator}{denominator}` | Division/Fractions | $\frac{1}{n^2}$ |
| `^` | Superscript | $x^2k + x^{2k}$ |
| `_` | Subscript | $x_2k + x_{2k}$ |
| `_^` | Superscript and subscript | $x_1^2 \text{(Stacked)} + x^4_5 \text{(Stacked)} + {x_7}^{6} \text{(Offset)}$ |
| `( ... )` | Parentheses 1 | $\displaystyle (\sum_{n=0}^N(\frac{1}{a+b})^2)$ |
| `\left( ... \right)` | Parentheses 2: Always in pairs | $\displaystyle \left(\sum_{n=0}^N\left(\frac{1}{a+b}\right)^2\right)$ |
| `\left( ... \right.` | When you need only the left parenthesis | $\displaystyle \left( x \right.$ |
| `\text{ Text }` | To display text in math mode - Don't forget the spaces | $a+b \text{ where } a \text{ and } b \text{ are variables }$ |
| `\alpha` | Greek Letters | $\alpha$ |
| `\phi` `\vartheta` `\varrho` `\varepsilon` | Different variations of Greek Letters | $φ \text{ - } \phi \\ θ \text{ - } \vartheta \\ ρ \text{ - } \varrho \\ ε \text{ - } \epsilon$ |
| `\mathbb{N}` | AMS Blackboard Font - For special symbols | $\text{Natural Numbers - } \mathbb{N}\\\text{integers - } \mathbb{I}$ |
| `\sum_{n=1}^\infty` | Sum for n=1 to infinity | $\sum_{n=1}^\infty$ |

---

# Tables And Arrays
- Table environment: For academic writing - Creates a labeled table
  - E.g.:<br>
    ```tex
    \begin{table}
    \caption{Table Title}
    \begin{tabular}{cc}
      Table Code
    \end{tabular}
    \end{table}
    ```

- Tabular Environment: General Environment for making tables
  - E.g.:<br>
    ```tex
    \begin{tabular}{cc}
      Table Code
    \end{tabular}
    ```

## 1. Tabular environment
| Command | Description | Values |
|---|---|---|
| `\begin{tabular} ... \end{tabular}` | Creation of table |
| `\begin{tabular}{alignment} ... \end{tabular}` | Alignment & Number of columns of table | - alignment = `lcr` -> 3 Columns: Left, Center and Right Justified<br>- alignment = `\|lc\|\|r` -> A vertical bar is added before Column 1 and double vertical bars are added before Column 3|

E.g.:<br>
```tex
\begin{tabular}{alignment} 
  text & text & text \\
  l    & c    & r
\end{tabular}
```
| | | |
|---|---|---|
| `&` | Moves to next cell |
| `\\` | Starts a new row |
| `\hline` | Creates horizontal line directly below the command |
| `\hline \hline` | Creates double horizontal line directly below the command |

Package: **hhline**
`\hhline{ commands }`:
- `=` : A double hline
- `-` : A single hline
- `~` : A column without hline
- `|` : A vline which 'cuts' through a double/single hline
- `:` : A vline which is broken by a double line
- `#` : A double hline segment between two vlines
- `t` : The top half of a double hline segment
- `b` : The bottom half of a double hline segment

### 1.1. Useful Packages
- booktabs: More attractive tables
- tabularx: Controlling width of columns
- colortbl: Coloring table
- longtable: Creating table that spans across multiple pages

## 2. Array Environment
- Arrays work like tables (Use same commands)
- Array must be in math mode

E.g.:
```tex
\begin{align*}
\begin{array}{ccc} 
  a_{11} & a_{12} & a_{13} \\
  a_{21} & a_{22} & a_{23}
\end{array}
\end{align*}
```

Result:
$$
\left( \begin{array}{c|cc} 
  a_{11} & a_{12} & \cdots \\
  \hline
  a_{21} & a_{22} & \cdots \\
  \vdots & \vdots & \ddots
\end{array} \right)
$$

| Command | Description |
|---|---|
| `\begin{pmatrix}` | Used instead of `array`. Includes the parentheses. No need to declare no. of columns |
| `\begin{bmatrix}` | Used instead of `array`. Includes the brackets. No need to declare no. of columns |
| `\cdots` | Adds three dots vertically |
| `\vdots` | Adds three dots vertcally |
| `\ddots` | Adds three dots diagonically |
| `\iddots` | Mirrored `\ddots`<br>Package: **mathdots** |

## 3. Merging Columns and Rows

### 3.1. Columns
`multicolumn{num_of_columns}{alignment}{contents}`:

```tex
\begin{tabular}{|c|c|c|}
\hline
Text & Text & Text \\
\multicolumn{3}{|c|}{Text} \\
Text & \multicolumn{2}{|l|}{Text} \\
\hline
\end{tabular}
```

### 3.2. Rows
Package: **multirow**

`\multirow[ vertical_alignment=t/c (Default) /b ]{ num_ rows }{ width=*/Xcm }{ contents }`:

```tex
\begin{tabular}{|c|c|}
\hline
\multirow[c]{3}{*}{Text} & Text \\
 & Text \\
\hline
\end{tabular}
```

---

# Calculus Notation

## 1. Functions
| Command | Description | Result |
|---|---|---|
| `\cdots` | Adds three dots | $f(x) = a_1 + a_2 + \cdots + a_n$ |
| `\sin(x)` | sin | $\sin(x) \text{ / } \sin{x} \text{ / } sinx \text{ (Not correct) }$ |
| `\operatorname{fun}(x)` | Create custom function | $\operatorname{fun}(x)$ |
| `\lim_{x \to X}` | Limit | $\lim_{x \to \infty} \text{ / } {\displaystyle \lim_{x \to \infty}}$ |
| `\to` | Arrow | $\to$ |
| `\infty` / `\infin` | infinity | $\infty$ |
| `\sum_{n=1}^{N}` | Sum | $\sum_{n=1}^{N} \text{ / } {\displaystyle \sum_{n=1}^{N}}$ |
| `\substack{}` | Adds more lines of information below | $\sum_{\substack{n=1 \\ n \text{ odd}}}^{N} \text{ / } {\displaystyle \sum_{\substack{n=1 \\ n \text{ odd}}}^{N}}$ |
| `\int` / `\iint` / `\iiint` | Single/Double/Triple integral | $\int \text{ / } \iint \text{ / } \iiint$ |
| `\int_{0}^{\infty}` | Integral with limits | $\int_0^\infty \text{ / } {\displaystyle \int_0^\infty \text{ / } \int \limits_0^\infty}$ |
| `\,` / `\:` / `\;` / `\quad` | Spacing commands | $\int \operatorname{f}(x)dx \text{ / } \int \operatorname{f}(x) \, dx \text{ / } \\ \int \operatorname{f}(x) \: dx \text{ / } \int \operatorname{f}(x) \; dx \text{ / } \\ \int \operatorname{f}(x) \quad dx$ |
| `\mathrm{}` | Roman font | $\int \operatorname{f}(x)\,\mathrm{d}x$ |
| | Different format of integrals | $\displaystyle \int_a^b \operatorname{f}(x)\,dx = \operatorname{F}(x) \bigg \vert_a^b \\ \space \\ \textstyle \int_a^b \operatorname{f}(x)\,dx = \operatorname{F}(x) \big \vert_a^b$ |
| `\partial` | Partial Derivatives | $\displaystyle \frac{\partial f}{\partial x}$ |
| `'` / `''` / `^{(n)}` | Prime (Langrangian) Notation of derivatives | $f'(x) \text{ / } f^{(n)}x$ |
| `\dot` / `\ddot` | Doot (Newtonian) Notation of derivatives | $\dot{\operatorname{f}}(x) \text{ / } \ddot{f}(x)$ |
| `\vec{}` / `\langle ... \rangle` | Vectors | $\vec{r}(t) = \langle x(t), y(t), z(t) \rangle$ |
| `\vv{}` | Vectors<br>Package: **esvect** |
| `\vv*{}` | Vector ignores subscripts (Smaller width of arrow) |
| `\nabla` | Upside Delta | $\nabla$ \
| `\oint` | Close Integral | $\oint$ |

---

# Miscellaneous Notation
