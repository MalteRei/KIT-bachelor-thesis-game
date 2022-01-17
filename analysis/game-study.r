# load the ARTool library into memory
library(ARTool)

# read a data table into variable 'df'
df <- read.csv("../study-results/spatial_presence_medians.csv") # assumes file is in working directory

# assume 'S' is the name of the subjects column
# assume 'X1' is the name of the first factor column
# assume 'X2' is the name of the second factor column
# assume 'X3' is the name of the third factor column
# assume 'Y' is the name of the response column
# run the ART procedure on 'df'
m = art(spatial_presence_median ~ factor(haptic_treatment_order) * factor(treatment) + (1|id_participant), data=df) # linear mixed model syntax; see lme4::lmer
anova(m)