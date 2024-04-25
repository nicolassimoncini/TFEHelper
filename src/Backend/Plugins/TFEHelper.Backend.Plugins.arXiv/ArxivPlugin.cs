using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.arXiv.Classes;
using TFEHelper.Backend.Plugins.arXiv.Extensions;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Common.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Tools;

namespace TFEHelper.Backend.Plugins.arXiv
{
    public class ArxivPlugin : IPublicationsCollectorPlugin, IParametersTypesExposser
    {

        public string Name => "arXiv plugin";
        public Version Version => new Version(1, 0, 0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "API adapter for arXiv";

        private ILogger _logger;
        private PluginConfigurationController _config;

        public bool StartUp(ILogger logger)
        {
            _logger = logger;
            _config = new PluginConfigurationController(_logger);

            return true;
        }

        public bool IsOnline()
        {
            return true;
        }

        public async Task<IEnumerable<PublicationPLG>> GetPublicationsAsync(PublicationsCollectorParametersPLG searchParameters, CancellationToken cancellationToken = default)
        {
            var api = new ArxivAPIWrapper(_logger);
            var entries = await api.GetAllRecordsAsync(searchParameters, _config, cancellationToken);

            // mapear los resultados a la lista de PublicationPLG...
            var publications = new List<PublicationPLG>();
            foreach (var entry in entries)
            {
                publications.Add(new PublicationPLG() {
                    Abstract = entry.Summary,
                    Authors = entry.Authors?.ToString(", ", (x) => { return x.Name; }),
                    DOI = entry.DOI,
                    ISBN = null,
                    ISSN = null,
                    Key = null,
                    Keywords = null,
                    Pages = null,
                    Source = SearchSourcePLGType.arXiv,
                    Title = entry.Title,
                    Type = BibTeXPublicationPLGType.Article,
                    URL = entry.Id,
                    Year = entry.Published.Year                    
                });
            }

            return publications;
        }

        public async Task<GlobalParametersContainer> GetParametersTypesAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var container = new GlobalParametersContainer();

                // Computer Science
                container.CollectionValued.Add("Subjects", "Artificial Intelligence", "cs.AI");
                container.CollectionValued.Add("Subjects", "Hardware Architecture", "cs.AR");
                container.CollectionValued.Add("Subjects", "Computational Complexity", "cs.CC");
                container.CollectionValued.Add("Subjects", "Computational Engineering, Finance, and Science", "cs.CE");
                container.CollectionValued.Add("Subjects", "Computational Geometry", "cs.CG");
                container.CollectionValued.Add("Subjects", "Computation and Language", "cs.CL");
                container.CollectionValued.Add("Subjects", "Cryptography and Security", "cs.CR");
                container.CollectionValued.Add("Subjects", "Computer Vision and Pattern Recognition", "cs.CV");
                container.CollectionValued.Add("Subjects", "Computers and Society", "cs.CY");
                container.CollectionValued.Add("Subjects", "Databases", "cs.DB");
                container.CollectionValued.Add("Subjects", "Distributed, Parallel, and Cluster Computing", "cs.DC");
                container.CollectionValued.Add("Subjects", "Digital Libraries", "cs.DL");
                container.CollectionValued.Add("Subjects", "Discrete Mathematics", "cs.DM");
                container.CollectionValued.Add("Subjects", "Data Structures and Algorithms", "cs.DS");
                container.CollectionValued.Add("Subjects", "Emerging Technologies", "cs.ET");
                container.CollectionValued.Add("Subjects", "Formal Languages and Automata Theory", "cs.FL");
                container.CollectionValued.Add("Subjects", "General Literature", "cs.GL");
                container.CollectionValued.Add("Subjects", "Graphics", "cs.GR");
                container.CollectionValued.Add("Subjects", "Computer Science and Game Theory", "cs.GT");
                container.CollectionValued.Add("Subjects", "Human-Computer Interaction", "cs.HC");
                container.CollectionValued.Add("Subjects", "Information Retrieval", "cs.IR");
                container.CollectionValued.Add("Subjects", "Information Theory", "cs.IT");
                container.CollectionValued.Add("Subjects", "Machine Learning", "cs.LG");
                container.CollectionValued.Add("Subjects", "Logic in Computer Science", "cs.LO");
                container.CollectionValued.Add("Subjects", "Multiagent Systems", "cs.MA");
                container.CollectionValued.Add("Subjects", "Multimedia", "cs.MM");
                container.CollectionValued.Add("Subjects", "Mathematical Software", "cs.MS");
                container.CollectionValued.Add("Subjects", "Numerical Analysis", "cs.NA");
                container.CollectionValued.Add("Subjects", "Neural and Evolutionary Computing", "cs.NE");
                container.CollectionValued.Add("Subjects", "Networking and Internet Architecture", "cs.NI");
                container.CollectionValued.Add("Subjects", "Other Computer Science", "cs.OH");
                container.CollectionValued.Add("Subjects", "Operating Systems", "cs.OS");
                container.CollectionValued.Add("Subjects", "Performance", "cs.PF");
                container.CollectionValued.Add("Subjects", "Programming Languages", "cs.PL");
                container.CollectionValued.Add("Subjects", "Robotics", "cs.RO");
                container.CollectionValued.Add("Subjects", "Symbolic Computation", "cs.SC");
                container.CollectionValued.Add("Subjects", "Sound", "cs.SD");
                container.CollectionValued.Add("Subjects", "Software Engineering", "cs.SE");
                container.CollectionValued.Add("Subjects", "Social and Information Networks", "cs.SI");
                container.CollectionValued.Add("Subjects", "Systems and Control", "cs.SY");

                // Economics
                container.CollectionValued.Add("Subjects", "Econometrics", "econ.EM");
                container.CollectionValued.Add("Subjects", "General Economics", "econ.GN");
                container.CollectionValued.Add("Subjects", "Theoretical Economics", "econ.TH");

                // Electrical Engineering and Systems Science
                container.CollectionValued.Add("Subjects", "Audio and Speech Processing", "eess.AS");
                container.CollectionValued.Add("Subjects", "Image and Video Processing", "eess.IV");
                container.CollectionValued.Add("Subjects", "Signal Processing", "eess.SP");
                container.CollectionValued.Add("Subjects", "Systems and Control", "eess.SY");

                // Mathematics
                container.CollectionValued.Add("Subjects", "Commutative Algebra", "math.AC");
                container.CollectionValued.Add("Subjects", "Algebraic Geometry", "math.AG");
                container.CollectionValued.Add("Subjects", "Analysis of PDEs", "math.AP");
                container.CollectionValued.Add("Subjects", "Algebraic Topology", "math.AT");
                container.CollectionValued.Add("Subjects", "Classical Analysis and ODEs", "math.CA");
                container.CollectionValued.Add("Subjects", "Combinatorics", "math.CO");
                container.CollectionValued.Add("Subjects", "Category Theory", "math.CT");
                container.CollectionValued.Add("Subjects", "Complex Variables", "math.CV");
                container.CollectionValued.Add("Subjects", "Differential Geometry", "math.DG");
                container.CollectionValued.Add("Subjects", "Dynamical Systems", "math.DS");
                container.CollectionValued.Add("Subjects", "Functional Analysis", "math.FA");
                container.CollectionValued.Add("Subjects", "General Mathematics", "math.GM");
                container.CollectionValued.Add("Subjects", "General Topology", "math.GN");
                container.CollectionValued.Add("Subjects", "Group Theory", "math.GR");
                container.CollectionValued.Add("Subjects", "Geometric Topology", "math.GT");
                container.CollectionValued.Add("Subjects", "History and Overview", "math.HO");
                container.CollectionValued.Add("Subjects", "Information Theory", "math.IT");
                container.CollectionValued.Add("Subjects", "K-Theory and Homology", "math.KT");
                container.CollectionValued.Add("Subjects", "Logic", "math.LO");
                container.CollectionValued.Add("Subjects", "Metric Geometry", "math.MG");
                container.CollectionValued.Add("Subjects", "Mathematical Physics", "math.MP");
                container.CollectionValued.Add("Subjects", "Numerical Analysis", "math.NA");
                container.CollectionValued.Add("Subjects", "Number Theory", "math.NT");
                container.CollectionValued.Add("Subjects", "Operator Algebras", "math.OA");
                container.CollectionValued.Add("Subjects", "Optimization and Control", "math.OC");
                container.CollectionValued.Add("Subjects", "Probability", "math.PR");
                container.CollectionValued.Add("Subjects", "Quantum Algebra", "math.QA");
                container.CollectionValued.Add("Subjects", "Rings and Algebras", "math.RA");
                container.CollectionValued.Add("Subjects", "Representation Theory", "math.RT");
                container.CollectionValued.Add("Subjects", "Symplectic Geometry", "math.SG");
                container.CollectionValued.Add("Subjects", "Spectral Theory", "math.SP");
                container.CollectionValued.Add("Subjects", "Statistics Theory", "math.ST");

                // Physics
                // Astrophysics (astro-ph)
                container.CollectionValued.Add("Subjects", "Cosmology and Nongalactic Astrophysics", "astro-ph.CO");
                container.CollectionValued.Add("Subjects", "Earth and Planetary Astrophysics", "astro-ph.EP");
                container.CollectionValued.Add("Subjects", "Astrophysics of Galaxies", "astro-ph.GA");
                container.CollectionValued.Add("Subjects", "High Energy Astrophysical Phenomena", "astro-ph.HE");
                container.CollectionValued.Add("Subjects", "Instrumentation and Methods for Astrophysics", "astro-ph.IM");
                container.CollectionValued.Add("Subjects", "Solar and Stellar Astrophysics", "astro-ph.SR");

                // Condensed Matter (cond-mat)
                container.CollectionValued.Add("Subjects", "Disordered Systems and Neural Networks", "cond-mat.dis-nn");
                container.CollectionValued.Add("Subjects", "Mesoscale and Nanoscale Physics", "cond-mat.mes-hall");
                container.CollectionValued.Add("Subjects", "Materials Science", "cond-mat.mtrl-sci");
                container.CollectionValued.Add("Subjects", "Other Condensed Matter", "cond-mat.other");
                container.CollectionValued.Add("Subjects", "Quantum Gases", "cond-mat.quant-gas");
                container.CollectionValued.Add("Subjects", "Soft Condensed Matter", "cond-mat.soft");
                container.CollectionValued.Add("Subjects", "Statistical Mechanics", "cond-mat.stat-mech");
                container.CollectionValued.Add("Subjects", "Strongly Correlated Electrons", "cond-mat.str-el");
                container.CollectionValued.Add("Subjects", "Superconductivity", "cond-mat.supr-con");

                // General Relativity and Quantum Cosmology (gr-qc)
                container.CollectionValued.Add("Subjects", "General Relativity and Quantum Cosmology", "gr-qc");

                // High Energy Physics - Experiment (hep-ex)
                container.CollectionValued.Add("Subjects", "High Energy Physics - Experiment", "hep-ex");

                // High Energy Physics - Lattice (hep-lat)
                container.CollectionValued.Add("Subjects", "High Energy Physics - Lattice", "hep-lat");

                // High Energy Physics - Phenomenology (hep-ph)
                container.CollectionValued.Add("Subjects", "High Energy Physics - Phenomenology", "hep-ph");

                // High Energy Physics - Theory (hep-th)
                container.CollectionValued.Add("Subjects", "High Energy Physics - Theory", "hep-th");

                // Mathematical Physics (math-ph)
                container.CollectionValued.Add("Subjects", "Mathematical Physics", "math-ph");

                // Nonlinear Sciences (nlin)
                container.CollectionValued.Add("Subjects", "Adaptation and Self-Organizing Systems", "nlin.AO");
                container.CollectionValued.Add("Subjects", "Chaotic Dynamics", "nlin.CD");
                container.CollectionValued.Add("Subjects", "Cellular Automata and Lattice Gases", "nlin.CG");
                container.CollectionValued.Add("Subjects", "Pattern Formation and Solitons", "nlin.PS");
                container.CollectionValued.Add("Subjects", "Exactly Solvable and Integrable Systems", "nlin.SI");

                // Nuclear Experiment (nucl-ex)
                container.CollectionValued.Add("Subjects", "Nuclear Experiment", "nucl-ex");

                // Nuclear Theory (nucl-th)
                container.CollectionValued.Add("Subjects", "Nuclear Theory", "nucl-th");

                // Physics (physics)
                container.CollectionValued.Add("Subjects", "Accelerator Physics", "physics.acc-ph");
                container.CollectionValued.Add("Subjects", "Atmospheric and Oceanic Physics", "physics.ao-ph");
                container.CollectionValued.Add("Subjects", "Applied Physics", "physics.app-ph");
                container.CollectionValued.Add("Subjects", "Atomic and Molecular Clusters", "physics.atm-clus");
                container.CollectionValued.Add("Subjects", "Atomic Physics", "physics.atom-ph");
                container.CollectionValued.Add("Subjects", "Biological Physics", "physics.bio-ph");
                container.CollectionValued.Add("Subjects", "Chemical Physics", "physics.chem-ph");
                container.CollectionValued.Add("Subjects", "Classical Physics", "physics.class-ph");
                container.CollectionValued.Add("Subjects", "Computational Physics", "physics.comp-ph");
                container.CollectionValued.Add("Subjects", "Data Analysis, Statistics and Probability", "physics.data-an");
                container.CollectionValued.Add("Subjects", "Physics Education", "physics.ed-ph");
                container.CollectionValued.Add("Subjects", "Fluid Dynamics", "physics.flu-dyn");
                container.CollectionValued.Add("Subjects", "General Physics", "physics.gen-ph");
                container.CollectionValued.Add("Subjects", "Geophysics", "physics.geo-ph");
                container.CollectionValued.Add("Subjects", "History and Philosophy of Physics", "physics.hist-ph");
                container.CollectionValued.Add("Subjects", "Instrumentation and Detectors", "physics.ins-det");
                container.CollectionValued.Add("Subjects", "Medical Physics", "physics.med-ph");

                // physics.optics (Optics)
                container.CollectionValued.Add("Subjects", "Plasma Physics", "physics.plasm-ph");
                container.CollectionValued.Add("Subjects", "Popular Physics", "physics.pop-ph");
                container.CollectionValued.Add("Subjects", "Physics and Society", "physics.soc-ph");
                container.CollectionValued.Add("Subjects", "Space Physics", "physics.space-ph");

                // Quantum Physics (quant-ph)
                container.CollectionValued.Add("Subjects", "Quantum Physics", "quant-ph");

                // Quantitative Biology
                container.CollectionValued.Add("Subjects", "Biomolecules", "q-bio.BM");
                container.CollectionValued.Add("Subjects", "Cell Behavior", "q-bio.CB");
                container.CollectionValued.Add("Subjects", "Genomics", "q-bio.GN");
                container.CollectionValued.Add("Subjects", "Molecular Networks", "q-bio.MN");
                container.CollectionValued.Add("Subjects", "Neurons and Cognition", "q-bio.NC");
                container.CollectionValued.Add("Subjects", "Other Quantitative Biology", "q-bio.OT");
                container.CollectionValued.Add("Subjects", "Populations and Evolution", "q-bio.PE");
                container.CollectionValued.Add("Subjects", "Quantitative Methods", "q-bio.QM");
                container.CollectionValued.Add("Subjects", "Subcellular Processes", "q-bio.SC");
                container.CollectionValued.Add("Subjects", "Tissues and Organs", "q-bio.TO");

                // Quantitative Finance
                container.CollectionValued.Add("Subjects", "Computational Finance", "q-fin.CP");
                container.CollectionValued.Add("Subjects", "Economics", "q-fin.EC");
                container.CollectionValued.Add("Subjects", "General Finance", "q-fin.GN");
                container.CollectionValued.Add("Subjects", "Mathematical Finance", "q-fin.MF");
                container.CollectionValued.Add("Subjects", "Portfolio Management", "q-fin.PM");
                container.CollectionValued.Add("Subjects", "Pricing of Securities", "q-fin.PR");
                container.CollectionValued.Add("Subjects", "Risk Management", "q-fin.RM");
                container.CollectionValued.Add("Subjects", "Statistical Finance", "q-fin.ST");
                container.CollectionValued.Add("Subjects", "Trading and Market Microstructure", "q-fin.TR");

                // Statistics
                container.CollectionValued.Add("Subjects", "Applications", "stat.AP");
                container.CollectionValued.Add("Subjects", "Computation", "stat.CO");

                // Algorithms, Simulation, Visualization
                container.CollectionValued.Add("Subjects", "Methodology", "stat.ME");
                container.CollectionValued.Add("Subjects", "Machine Learning", "stat.ML");
                container.CollectionValued.Add("Subjects", "Other Statistics", "stat.OT");
                container.CollectionValued.Add("Subjects", "Statistics Theory", "stat.TH");

                return container;
            }, cancellationToken);
        }
    }
}