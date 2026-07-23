using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TypeBeatScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;

    public KMSelectable ActivateSel;

    public GameObject[] ParentObjs;
    public TextMesh[] ScreenTexts;
    public GameObject SwitchObj;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;

    private static readonly string[] _wordList = new string[]
    {
        "ABNORMAL", "ABORTION", "ABRUPTLY", "ABSOLUTE", "ABSORBED", "ABSTRACT", "ABUNDANT", "ACADEMIC", "ACCEPTED", "ACCIDENT", "ACCURACY", "ACCURATE", "ACCUSING", "ACHIEVED", "ACOUSTIC", "ACQUIRED", "ACTIVELY", "ACTIVIST", "ACTIVITY", "ACTUALLY", "ADAPTIVE", "ADDITION", "ADEQUACY", "ADEQUATE", "ADHESION", "ADHESIVE", "ADJACENT", "ADJUSTED", "ADMITTED", "ADOPTING", "ADOPTION", "ADULTERY", "ADVANCED", "ADVISORY", "ADVOCACY", "ADVOCATE", "AFFECTED", "AFFINITY", "AFFLUENT", "AIRBORNE", "AIRCRAFT", "AIRFIELD", "ALKALINE", "ALLERGIC", "ALLIANCE", "ALLOCATE", "ALMIGHTY", "ALPHABET", "ALTERING", "ALTHOUGH", "ALTITUDE", "AMBITION", "AMENABLE", "AMERICAN", "ANALOGUE", "ANALYSIS", "ANCESTOR", "ANIMATED", "ANNOUNCE", "ANNUALLY", "ANOREXIA", "ANSWERED", "ANTELOPE", "ANTIBODY", "ANTIDOTE", "ANYTHING", "ANYWHERE", "APPALLED", "APPARENT", "APPENDIX", "APPETITE", "APPLAUSE", "APPROACH", "APPROVAL", "APPROVED", "AQUARIUM", "ARGUABLY", "ARGUMENT", "ARMCHAIR", "ARMOURED", "AROMATIC", "ARRANGED", "ARRESTED", "ARRIVING", "ARROGANT", "ARTISTIC", "ASBESTOS", "ASSASSIN", "ASSEMBLE", "ASSEMBLY", "ASSERTED", "ASSESSED", "ASSIGNED", "ASSISTED", "ASSUMING", "ATHLETIC", "ATTACHED", "ATTACKED", "ATTACKER", "ATTAINED", "ATTENDED", "ATTITUDE", "ATTORNEY", "AUDIENCE", "AUDITION", "AUTOMATE", "AUTONOMY", "AVIATION", "AWAITING", "BACHELOR", "BACKBONE", "BACKDROP", "BACKPACK", "BACKSIDE", "BACKWARD", "BACKYARD", "BACTERIA", "BALANCED", "BALLROOM", "BANKRUPT", "BARBECUE", "BAREFOOT", "BARRACKS", "BASEBALL", "BASELINE", "BASEMENT", "BATHROOM", "BATTERED", "BATTLING", "BECOMING", "BEGINNER", "BETRAYAL", "BETRAYED", "BEVERAGE", "BIBLICAL", "BILLIARD", "BIRTHDAY", "BITTERLY", "BLEEDING", "BLESSING", "BLIZZARD", "BLOCKADE", "BLOCKING", "BLOOMING", "BLOWFISH", "BOARDING", "BOOKCASE", "BOOKMARK", "BOOKSHOP", "BORROWED", "BORROWER", "BOTHERED", "BOUNCING", "BOUNDARY", "BRACELET", "BREAKING", "BREEDING", "BRIEFING", "BRIGHTLY", "BRINGING", "BROCCOLI", "BROCHURE", "BROWNING", "BRUSHING", "BRUTALLY", "BUILDING", "BULLETIN", "BUNGALOW", "BURGLARY", "BURSTING", "BUSINESS", "CALENDAR", "CAMPAIGN", "CANADIAN", "CANNABIS", "CAPACITY", "CARDINAL", "CARELESS", "CARNIVAL", "CARRIAGE", "CARRYING", "CASSETTE", "CASUALLY", "CASUALTY", "CATALYST", "CATCHING", "CATEGORY", "CATERING", "CATHOLIC", "CAUTIOUS", "CELLULAR", "CEMETERY", "CEREBRAL", "CEREMONY", "CERVICAL", "CHAIRMAN", "CHAMPION", "CHANGING", "CHAPLAIN", "CHARCOAL", "CHARGING", "CHARMING", "CHATTING", "CHECKING", "CHEERFUL", "CHEMICAL", "CHESTNUT", "CHILDISH", "CHILDREN", "CHLORINE", "CHOOSING", "CIRCULAR", "CITATION", "CIVILIAN", "CLAIMANT", "CLASSIFY", "CLEANING", "CLEARING", "CLEAVAGE", "CLERICAL", "CLIMATIC", "CLIMBING", "CLINGING", "CLINICAL", "CLOTHING", "COCKTAIL", "COERCION", "COHERENT", "COHESION", "COINCIDE", "COLLAPSE", "COLONIAL", "COLORFUL", "COLOURED", "COMBINED", "COMEBACK", "COMEDIAN", "COMMANDO", "COMMENCE", "COMMONLY", "COMMUNAL", "COMPILED", "COMPLAIN", "COMPLETE", "COMPOSED", "COMPOSER", "COMPOUND", "COMPRISE", "COMPUTER", "CONCEIVE", "CONCLUDE", "CONCRETE", "CONFINED", "CONFLICT", "CONFRONT", "CONFUSED", "CONGRESS", "CONQUEST", "CONSIDER", "CONSTANT", "CONSUMED", "CONSUMER", "CONTEMPT", "CONTINUE", "CONTRACT", "CONTRARY", "CONTRAST", "CONVINCE", "CORONARY", "CORRIDOR", "COSMETIC", "COUNTING", "COURTESY", "COVENANT", "COVERAGE", "COVERING", "CRACKING", "CRASHING", "CRAWLING", "CREATION", "CREATIVE", "CREATURE", "CREDIBLE", "CREDITED", "CREDITOR", "CREEPING", "CRESCENT", "CRIMINAL", "CRITERIA", "CRITICAL", "CRITIQUE", "CROSSING", "CROUCHED", "CULTURAL", "CUPBOARD", "CURRENCY", "CUSTOMER", "CYLINDER", "CYNICISM", "DAMAGING", "DARKENED", "DARKNESS", "DATABASE", "DAUGHTER", "DAYLIGHT", "DEADLINE", "DEALINGS", "DECEASED", "DECIDING", "DECISION", "DECISIVE", "DECLARED", "DECORATE", "DECREASE", "DEDICATE", "DEFEATED", "DEFENDER", "DEFIANCE", "DEFINING", "DEFINITE", "DELEGATE", "DELETION", "DELICACY", "DELICATE", "DELIVERY", "DEMENTIA", "DEMOCRAT", "DEPICTED", "DEPLOYED", "DEPRIVED", "DERELICT", "DESCRIBE", "DESERTED", "DESERVED", "DESIGNED", "DESIGNER", "DESTINED", "DETACHED", "DETAILED", "DETAINED", "DETECTED", "DETECTOR", "DEVIANCE", "DEVOTION", "DIABETES", "DIABETIC", "DIAGONAL", "DIALOGUE", "DIAMETER", "DICTATED", "DICTATOR", "DIMINISH", "DINOSAUR", "DIPLOMAT", "DIRECTED", "DIRECTLY", "DIRECTOR", "DISABLED", "DISAGREE", "DISASTER", "DISCLOSE", "DISCOUNT", "DISCOVER", "DISCREET", "DISCRETE", "DISGRACE", "DISGUISE", "DISLIKED", "DISORDER", "DISPATCH", "DISPOSAL", "DISPOSED", "DISPUTED", "DISSOLVE", "DISTANCE", "DISTASTE", "DISTINCT", "DISTRACT", "DISTRESS", "DISTRICT", "DIVERTED", "DIVIDEND", "DIVIDING", "DIVISION", "DIVORCED", "DOCTRINE", "DOCUMENT", "DOMESTIC", "DOMINANT", "DOMINATE", "DONATION", "DOORSTEP", "DOUBTFUL", "DOWNFALL", "DOWNLOAD", "DOWNWARD", "DRAGGING", "DRAINAGE", "DRAMATIC", "DRAWBACK", "DREADFUL", "DREAMING", "DRESSING", "DRIFTING", "DRINKING", "DROPPING", "DROWNING", "DURATION", "DWELLING", "DYNAMICS", "EARNINGS", "ECONOMIC", "EDUCATED", "EDUCATOR", "EFFICACY", "EGGPLANT", "EGYPTIAN", "EIGHTEEN", "ELECTION", "ELECTRIC", "ELECTRON", "ELEGANCE", "ELEPHANT", "ELEVATED", "ELEVATOR", "ELEVENTH", "ELIGIBLE", "ELOQUENT", "EMBEDDED", "EMERGING", "EMISSION", "EMPHASIS", "EMPHATIC", "EMPLOYED", "EMPLOYEE", "EMPLOYER", "ENABLING", "ENCLOSED", "ENDEAVOR", "ENDORSED", "ENFORCED", "ENGAGING", "ENGINEER", "ENHANCED", "ENLARGED", "ENORMOUS", "ENSEMBLE", "ENTIRELY", "ENTRANCE", "ENVELOPE", "ENVISAGE", "EPIDEMIC", "EPILOGUE", "EQUALITY", "EQUATION", "ERECTION", "ERUPTION", "ESCAPING", "ESTIMATE", "ETCETERA", "ETERNITY", "EUROPEAN", "EVACUATE", "EVALUATE", "EVENTUAL", "EVERYDAY", "EVERYONE", "EVIDENCE", "EXAMINED", "EXAMINER", "EXCHANGE", "EXCITING", "EXCLUDED", "EXECUTED", "EXERCISE", "EXISTING", "EXPANDED", "EXPECTED", "EXPELLED", "EXPLICIT", "EXPLODED", "EXPONENT", "EXPORTED", "EXPORTER", "EXPOSURE", "EXTENDED", "EXTERIOR", "EXTERNAL", "FABULOUS", "FACILITY", "FAIRNESS", "FAITHFUL", "FAMILIAR", "FAREWELL", "FARMLAND", "FARTHEST", "FAVORITE", "FAVOURED", "FEARLESS", "FEASIBLE", "FEATURED", "FEEDBACK", "FEMININE", "FEMINISM", "FEMINIST", "FESTIVAL", "FIBROSIS", "FIDDLING", "FIERCELY", "FIGHTING", "FINALIZE", "FINISHED", "FIREWALL", "FIRMWARE", "FIXATION", "FLAMINGO", "FLASHING", "FLAWLESS", "FLEXIBLE", "FLOATING", "FLOODING", "FLOURISH", "FOCUSING", "FOLLOWER", "FOOTBALL", "FOOTPATH", "FORCEFUL", "FORCIBLY", "FORECAST", "FOREHEAD", "FOREMOST", "FORENSIC", "FORESTRY", "FORMALLY", "FORMERLY", "FORTRESS", "FORWARDS", "FOUNTAIN", "FOURTEEN", "FRACTION", "FRACTURE", "FRAGMENT", "FRAGRANT", "FREEZING", "FREQUENT", "FRICTION", "FRIENDLY", "FRIGHTEN", "FRONTIER", "FROWNING", "FRUITFUL", "FUNCTION", "GALACTIC", "GARDENER", "GARGOYLE", "GARRISON", "GASOLINE", "GATHERED", "GEMSTONE", "GENERATE", "GENEROUS", "GEOMETRY", "GIGANTIC", "GIVEAWAY", "GLANCING", "GLORIOUS", "GLOSSARY", "GOLDFISH", "GOODNESS", "GOODWILL", "GORGEOUS", "GOVERNOR", "GRABBING", "GRACEFUL", "GRACIOUS", "GRADIENT", "GRADUATE", "GRAFFITI", "GRANDDAD", "GRANDEUR", "GRANDSON", "GRAPHICS", "GRAPHITE", "GRASPING", "GRATEFUL", "GREATEST", "GREETING", "GRIDLOCK", "GRIEVOUS", "GRINNING", "GRIPPING", "GUARDIAN", "GUIDANCE", "GULLIBLE", "HABITUAL", "HALFTIME", "HALLMARK", "HANDBOOK", "HANDICAP", "HANDLING", "HANDMADE", "HANDSOME", "HARDNESS", "HARDSHIP", "HARDWARE", "HARMLESS", "HARMONIC", "HATCHING", "HAZELNUT", "HEADACHE", "HEADLINE", "HEAVENLY", "HEDGEHOG", "HEGEMONY", "HEIRLOOM", "HELPLESS", "HERITAGE", "HESITANT", "HESITATE", "HILLSIDE", "HISTORIC", "HITHERTO", "HOLOGRAM", "HOMELAND", "HOMELESS", "HOMEWORK", "HONESTLY", "HONORARY", "HOPELESS", "HORRIBLE", "HORRIBLY", "HORRIFIC", "HORSEMAN", "HOSPITAL", "HOVERING", "HUMANITY", "HUMIDITY", "HUMILITY", "HUMOROUS", "HURRYING", "HYDROGEN", "HYPNOSIS", "HYSTERIA", "IDEALISM", "IDENTIFY", "IDENTITY", "IDEOLOGY", "IGNORANT", "IGNORING", "ILLUSION", "IMAGINED", "IMMATURE", "IMMINENT", "IMMUNITY", "IMPERIAL", "IMPLICIT", "IMPORTED", "IMPOSING", "IMPROPER", "IMPROVED", "INACTIVE", "INCIDENT", "INCLINED", "INCLUDED", "INCOMING", "INCREASE", "INDECENT", "INDICATE", "INDIRECT", "INDUSTRY", "INFAMOUS", "INFANTRY", "INFECTED", "INFERIOR", "INFINITE", "INFINITY", "INFLATED", "INFORMAL", "INFORMED", "INFUSION", "INHERENT", "INITIATE", "INNOCENT", "INNOVATE", "INSECURE", "INSERTED", "INSPIRED", "INSTANCE", "INSTINCT", "INSTRUCT", "INTEGRAL", "INTENDED", "INTENTLY", "INTERACT", "INTEREST", "INTERIOR", "INTERNAL", "INTERVAL", "INTIMACY", "INTIMATE", "INTRIGUE", "INTRUDER", "INVASION", "INVENTED", "INVENTOR", "INVERTED", "INVESTED", "INVESTOR", "INVITING", "INVOLVED", "ISOLATED", "JALAPENO", "JAPANESE", "JEALOUSY", "JEOPARDY", "JEWELLER", "JIGGLING", "JINGLING", "JOKESTER", "JOKINGLY", "JOYOUSLY", "JOYSTICK", "JUDGMENT", "JUDICIAL", "JUNCTION", "JUNKYARD", "JUVENILE", "KANGAROO", "KEYBOARD", "KILOGRAM", "KINDNESS", "KNAPSACK", "KNICKERS", "KNITTING", "KNOCKING", "KNOCKOFF", "LABELLED", "LABOURER", "LANDLADY", "LANDLORD", "LANDMARK", "LANGUAGE", "LATITUDE", "LAUGHING", "LAUGHTER", "LAVATORY", "LAVENDER", "LEARNING", "LECTURER", "LEMONADE", "LEVERAGE", "LICENSED", "LIFEBOAT", "LIFELONG", "LIFESPAN", "LIFETIME", "LIGHTING", "LIKEWISE", "LIMITING", "LIPSTICK", "LISTENER", "LITERACY", "LITERARY", "LITERATE", "LOCALITY", "LOCALIZE", "LOCATION", "LORDSHIP", "LOVINGLY", "LOWERING", "LOYALIST", "LUMINOUS", "LUNCHEON", "MAGAZINE", "MAGICIAN", "MAGNETIC", "MAHOGANY", "MAINLAND", "MAINTAIN", "MAJESTIC", "MAJORITY", "MANAGING", "MANEUVER", "MANIFEST", "MANPOWER", "MARATHON", "MARCHING", "MARGINAL", "MARITIME", "MARKEDLY", "MARKETED", "MARRIAGE", "MARRYING", "MARSHALL", "MASSACRE", "MATCHING", "MATERIAL", "MATERNAL", "MATTRESS", "MATURITY", "MAXIMIZE", "MEANTIME", "MEASURED", "MECHANIC", "MEDICINE", "MEDIEVAL", "MEMBRANE", "MENTALLY", "MERCHANT", "METALLIC", "METAPHOR", "MIDFIELD", "MIDNIGHT", "MILITANT", "MILITARY", "MINIMIZE", "MINISTER", "MINISTRY", "MINORITY", "MISCHIEF", "MISTAKEN", "MISTRESS", "MOBILITY", "MODELING", "MODERATE", "MODESTLY", "MODIFIED", "MOISTURE", "MOLECULE", "MOMENTUM", "MONARCHY", "MONETARY", "MONOPOLY", "MONOXIDE", "MONUMENT", "MOORLAND", "MORALITY", "MOREOVER", "MORTGAGE", "MOSQUITO", "MOTIVATE", "MOTORWAY", "MOUNTAIN", "MOUNTING", "MOUTHFUL", "MOVEMENT", "MULTIPLE", "MULTIPLY", "MURDERED", "MURDERER", "MURMURED", "MUSCULAR", "MUSHROOM", "MUSICIAN", "MUTATION", "MUTTERED", "MUTUALLY", "MYSTICAL", "NARROWED", "NARROWLY", "NATIONAL", "NAVIGATE", "NECKLACE", "NEEDLESS", "NEGATION", "NEGATIVE", "NEIGHBOR", "NEWCOMER", "NICKNAME", "NINETEEN", "NITROGEN", "NOBILITY", "NONSENSE", "NORMALLY", "NORTHERN", "NOTATION", "NOTEBOOK", "NOTICING", "NOTIFIED", "NOVELIST", "NOWADAYS", "NUISANCE", "NUMEROUS", "OBLIVION", "OBSERVED", "OBSERVER", "OBSESSED", "OBSIDIAN", "OBSOLETE", "OBSTACLE", "OCCASION", "OCCUPIED", "OCCUPIER", "OFFENDER", "OFFERING", "OFFICIAL", "OFFSHORE", "OINTMENT", "OLYMPICS", "OMISSION", "OPENNESS", "OPERATOR", "OPPONENT", "OPPOSING", "OPPOSITE", "OPTIMISM", "OPTIMIZE", "OPTIONAL", "ORDERING", "ORDINARY", "ORGANISM", "ORGANIZE", "ORIENTAL", "ORIGINAL", "ORNAMENT", "ORTHODOX", "OUTBREAK", "OUTBURST", "OUTGOING", "OUTLINED", "OUTRIGHT", "OUTSIDER", "OUTWARDS", "OVERCOAT", "OVERCOME", "OVERFLOW", "OVERHEAD", "OVERLOAD", "OVERRIDE", "OVERSEAS", "OVERTIME", "OVERTURE", "OVERVIEW", "PAINTING", "PAMPHLET", "PARADIGM", "PARADISE", "PARALLEL", "PARASITE", "PARENTAL", "PARTICLE", "PASSPORT", "PASSWORD", "PASTORAL", "PATHETIC", "PATIENCE", "PAVEMENT", "PAVILION", "PEACEFUL", "PECULIAR", "PEDIGREE", "PENTAGON", "PERCEIVE", "PERIODIC", "PERSONAL", "PERSUADE", "PERVERSE", "PETITION", "PHYSICAL", "PICTURED", "PIPELINE", "PLANNING", "PLANTING", "PLATFORM", "PLEADING", "PLEASANT", "PLEASING", "PLEASURE", "PLOTTING", "PLUMBING", "POIGNANT", "POINTING", "POLICING", "POLISHED", "POLITELY", "POLITICS", "PORTABLE", "PORTRAIT", "POSITION", "POSITIVE", "POSSIBLE", "POSSIBLY", "POSTCARD", "POSTPONE", "POWERFUL", "PRACTICE", "PREACHER", "PRECIOUS", "PREDATOR", "PREGNANT", "PREMIERE", "PREPARED", "PRESENCE", "PRESERVE", "PRESSING", "PRESSURE", "PRESTIGE", "PRETENCE", "PREVIOUS", "PRINTING", "PRIORITY", "PRISONER", "PROBABLE", "PROBABLY", "PROCEEDS", "PROCLAIM", "PRODUCED", "PRODUCER", "PROFOUND", "PROGRESS", "PROHIBIT", "PROLIFIC", "PROMISED", "PROMOTED", "PROMOTER", "PROMPTLY", "PROPERLY", "PROPERTY", "PROPHECY", "PROPOSAL", "PROPOSED", "PROSPECT", "PROTOCOL", "PROVIDED", "PROVIDER", "PROVINCE", "PROXIMAL", "PUBLICLY", "PUNISHED", "PUNITIVE", "PURCHASE", "PURSUING", "QUADRANT", "QUANTITY", "QUARRIES", "QUARTERS", "QUESTION", "QUILTING", "RADIATOR", "RAILROAD", "RAINFALL", "RANDOMLY", "RATIONAL", "RATTLING", "REACHING", "REACTION", "REACTIVE", "READABLE", "REALIZED", "REASSURE", "RECALLED", "RECEIVED", "RECEIVER", "RECENTLY", "RECEPTOR", "RECHARGE", "RECKLESS", "RECORDED", "RECORDER", "RECOURSE", "RECOVERY", "RECREATE", "REDEFINE", "REDESIGN", "REDIRECT", "REDUCING", "REFERRAL", "REFERRED", "REFINERY", "REFORMED", "REGIMENT", "REGIONAL", "REGISTER", "REGISTRY", "REGULATE", "REHEARSE", "REINDEER", "REJECTED", "RELATION", "RELATIVE", "RELAXING", "RELEASED", "RELEVANT", "RELIABLE", "RELIABLY", "RELIANCE", "RELIEVED", "RELIGION", "RELOCATE", "REMEDIAL", "REMEMBER", "REMINDER", "REMOTELY", "RENOVATE", "RENOWNED", "REPEATED", "REPLACED", "REPORTED", "REPORTER", "REQUIRED", "RESEARCH", "RESENTED", "RESERVED", "RESIDENT", "RESIDUAL", "RESIGNED", "RESISTOR", "RESOLVED", "RESOURCE", "RESPONSE", "RESTLESS", "RESTORED", "RESTRAIN", "RESTRICT", "RESTROOM", "RETAILER", "RETAINED", "RETIRING", "RETRIEVE", "RETURNED", "REVEALED", "REVEREND", "REVERSAL", "REVERSED", "REVIEWED", "REVISION", "RHETORIC", "RHYTHMIC", "RICHNESS", "RIGHTFUL", "RIGOROUS", "ROADSIDE", "ROMANTIC", "ROTATION", "RUEFULLY", "RUTHLESS", "SALESMAN", "SANCTION", "SANDWICH", "SAUCEPAN", "SAVAGELY", "SCARCELY", "SCENARIO", "SCHEDULE", "SCISSORS", "SCOTTISH", "SCRAMBLE", "SCREENED", "SCRUTINY", "SCULPTOR", "SEASONAL", "SECONDLY", "SECRETLY", "SECURELY", "SECURING", "SECURITY", "SEDIMENT", "SELECTED", "SEMANTIC", "SENSIBLE", "SENSIBLY", "SENTENCE", "SEPARATE", "SEQUENCE", "SERGEANT", "SETTLING", "SEVERELY", "SEVERITY", "SEXUALLY", "SHEPHERD", "SHIFTING", "SHILLING", "SHIPMENT", "SHIPPING", "SHOCKING", "SHOOTING", "SHOPPING", "SHORTAGE", "SHOULDER", "SHOUTING", "SHOWCASE", "SICKNESS", "SIDEWAYS", "SILENTLY", "SIMPLIFY", "SIMULATE", "SINGULAR", "SINISTER", "SITUATED", "SIXPENCE", "SIZEABLE", "SKELETAL", "SKELETON", "SLEEPING", "SLIGHTLY", "SLIPPERY", "SLIPPING", "SMOOTHLY", "SOCIALLY", "SOFTWARE", "SOLEMNLY", "SOLITARY", "SOLITUDE", "SOLUTION", "SOMEBODY", "SOMETIME", "SOMEWHAT", "SOOTHING", "SOUNDING", "SOUTHERN", "SPACIOUS", "SPEAKING", "SPECIFIC", "SPECIMEN", "SPECTRUM", "SPEEDING", "SPELLING", "SPENDING", "SPILLING", "SPINNING", "SPLENDID", "SPORADIC", "SPORTING", "SPURIOUS", "SQUADRON", "SQUEEZED", "STANDARD", "STANDING", "STARRING", "STARTING", "STARVING", "STEADILY", "STEALING", "STEERING", "STERLING", "STICKING", "STIMULUS", "STIRRING", "STOPPING", "STRAIGHT", "STRANGER", "STRATEGY", "STRENGTH", "STRESSED", "STRICKEN", "STRICTLY", "STRIKING", "STRIPPED", "STRIVING", "STROKING", "STRONGLY", "STRUGGLE", "STUBBORN", "STUDYING", "STUNNING", "SUBURBAN", "SUDDENLY", "SUFFERER", "SUFFRAGE", "SUITABLE", "SUITABLY", "SUITCASE", "SULPHATE", "SUNLIGHT", "SUNSHINE", "SUPERBLY", "SUPERIOR", "SUPPLIED", "SUPPLIER", "SUPPOSED", "SUPPRESS", "SURGICAL", "SURPRISE", "SURROUND", "SURVEYED", "SURVEYOR", "SURVIVAL", "SURVIVOR", "SWEATING", "SWEEPING", "SWIMMING", "SWINGING", "SYLLABLE", "SYLLABUS", "SYMBOLIC", "SYMMETRY", "SYMPATHY", "SYMPHONY", "SYNDROME", "SYSTEMIC", "TACKLING", "TACTICAL", "TAKEOVER", "TALENTED", "TALISMAN", "TANGIBLE", "TARGETED", "TAXATION", "TAXPAYER", "TEACHING", "TEAMWORK", "TEASPOON", "TEENAGER", "TELEPORT", "TEMPLATE", "TEMPORAL", "TEMPTING", "TENDENCY", "TENTACLE", "TERMINAL", "TERMINUS", "TERRACED", "TERRIBLE", "TERRIBLY", "TERRIFIC", "TEXTBOOK", "THANKFUL", "THEMATIC", "THEOLOGY", "THINKING", "THIRTEEN", "THOROUGH", "THOUSAND", "THREATEN", "THRILLED", "THRILLER", "THRIVING", "THROTTLE", "TIMELINE", "TOGETHER", "TOLERANT", "TOLERATE", "TOMORROW", "TORTOISE", "TOTALITY", "TOUCHING", "TOWERING", "TOWNSHIP", "TRACKING", "TRAILING", "TRAINING", "TRANQUIL", "TRANSFER", "TRANSMIT", "TRAVELED", "TRAVERSE", "TREASURE", "TRIANGLE", "TRIBUNAL", "TRILLION", "TRIMMING", "TROPICAL", "TROUBLED", "TROUSERS", "TUMBLING", "TURNOVER", "TUTORIAL", "TWILIGHT", "TWISTING", "ULTIMATE", "UMBRELLA", "UNBROKEN", "UNCOMMON", "UNDERDOG", "UNDERWAY", "UNEASILY", "UNFAIRLY", "UNIONISM", "UNIONIST", "UNIQUELY", "UNIVERSE", "UNLAWFUL", "UNLIKELY", "UNLISTED", "UNMARKED", "UNSPOKEN", "UNSTABLE", "UNTITLED", "UNUSABLE", "UNWANTED", "UPCOMING", "UPRISING", "UPSTAIRS", "URGENTLY", "USEFULLY", "USERNAME", "VACATION", "VALIDATE", "VALIDITY", "VALUABLE", "VANISHED", "VANQUISH", "VAPORIZE", "VARIABLE", "VARIANCE", "VELOCITY", "VENETIAN", "VERTICAL", "VERTICES", "VICINITY", "VIGILANT", "VIGOROUS", "VILLAGER", "VINEYARD", "VIOLENCE", "VIRTUOUS", "VISCOUNT", "VISITING", "VISUALLY", "VITALITY", "VOCALIST", "VOLATILE", "VOLCANIC", "VOLITION", "WAITRESS", "WARDROBE", "WARRANTY", "WASHROOM", "WASTEFUL", "WATCHDOG", "WATCHFUL", "WATCHING", "WEAKENED", "WEAKNESS", "WEARABLE", "WEIGHING", "WEIGHTED", "WEREWOLF", "WHATEVER", "WHENEVER", "WHEREVER", "WILDLIFE", "WINDMILL", "WINGSPAN", "WIRELESS", "WISHBONE", "WITHDRAW", "WITHDREW", "WITHHOLD", "WONDROUS", "WOODLAND", "WOODWORK", "WORKABLE", "WORKLOAD", "WORKSHOP", "WORRYING", "WRAPPING", "WRECKAGE", "WRETCHED", "WRITABLE", "XENOLITH", "YEARBOOK", "YIELDING", "YOURSELF", "YOUTHFUL", "ZUCCHINI"
    };

    private bool _isSelected;
    private string[] _currentInput = new string[8];

    private string _chosenWord;
    private int[] _randomOrderA;
    private int[] _randomOrderB;

    private bool _isActivated;
    private bool _switchFlipped;
    private Coroutine _switchAnimCoroutine;

    private const float _y = 0.001f;
    private const float _pulsed = 0.0175f;
    private const float _size = 0.015f;
    private const float _time = 0.333f;
    private int _currentIx = -1;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        var ModSelectable = GetComponent<KMSelectable>();
        ModSelectable.OnFocus += delegate () { _isSelected = true; };
        ModSelectable.OnDefocus += delegate () { _isSelected = false; };
        ActivateSel.OnInteract += ActivatePress;

        for (int i = 0; i < 8; i++)
            ScreenTexts[i].text = "";
    }

    private bool ActivatePress()
    {
        if (_moduleSolved || _isActivated)
            return false;

        _switchFlipped = !_switchFlipped;
        if (_switchAnimCoroutine != null)
            StopCoroutine(_switchAnimCoroutine);
        _switchAnimCoroutine = StartCoroutine(AnimateSwitch(_switchFlipped));

        _isActivated = true;
        _currentInput = new string[8] { "?", "?", "?", "?", "?", "?", "?", "?" };
        _chosenWord = _wordList.PickRandom();
        _randomOrderA = Enumerable.Range(0, 8).ToArray().Shuffle();
        _randomOrderB = Enumerable.Range(0, 8).ToArray().Shuffle();

        for (int i = 0; i < ScreenTexts.Length; i++)
        {
            ScreenTexts[i].text = _chosenWord[i].ToString();
            ParentObjs[i].transform.localScale = new Vector3(0, _y, 0);
        }

        // Debug.LogFormat("[Type Beat #{0}] Chosen word: {1}.", _moduleId, _chosenWord);
        // Debug.LogFormat("[Type Beat #{0}] Display order: {1}.", _moduleId, _randomOrderA.Select(i => i + 1).Join(", "));
        // Debug.LogFormat("[Type Beat #{0}] Input order: {1}. ({2})", _moduleId, _randomOrderB.Select(i => i + 1).Join(", "), _randomOrderB.Select(i => _chosenWord[i]).Join(", "));

        StartCoroutine(PlayAnimation());
        return false;
    }

    private IEnumerator AnimateSwitch(bool dir)
    {
        float rotStart = SwitchObj.transform.localEulerAngles.y;
        float rotEnd = dir ? -30f : 0f;
        if (rotStart - rotEnd > 180f)
            rotEnd += 360f;
        if (rotEnd - rotStart > 180f)
            rotStart += 360f;
        var duration = 0.1f;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            SwitchObj.transform.localEulerAngles = new Vector3(90f, Easing.InOutQuad(elapsed, rotStart, rotEnd, duration), 0f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        SwitchObj.transform.localEulerAngles = new Vector3(90f, rotEnd, 0f);
        yield break;
    }

    private IEnumerator PlayAnimation()
    {
        for (int i = 0; i < 8; i++)
            ScreenTexts[_randomOrderB[i]].color = new Color(1, 1, 1, 1);
        Audio.PlaySoundAtTransform("Activation", transform);
        yield return new WaitForSeconds(0.65f);
        for (int i = 0; i < 8; i++)
        {
            StartCoroutine(PulseIn(_randomOrderA[i]));
            yield return new WaitForSeconds(_time);
            StartCoroutine(PulseOut(_randomOrderA[i]));
            yield return new WaitForSeconds(_time);
        }
        for (int i = 0; i < 8; i++)
            ScreenTexts[i].text = "?";
        for (int i = 0; i < 8; i++)
        {
            _currentIx = _randomOrderB[i];
            var soFar = Enumerable.Range(0, i + 1).ToArray();
            for (int j = 0; j < soFar.Length; j++)
                StartCoroutine(PulseIn(_randomOrderB[j]));
            yield return new WaitForSeconds(_time);
            for (int j = 0; j < soFar.Length; j++)
                StartCoroutine(PulseIn(_randomOrderB[j]));
            yield return new WaitForSeconds(_time);
            ScreenTexts[_randomOrderB[i]].color = new Color(0.7f, 0.7f, 0.7f, 1);
        }
        CheckAnswer();
        if (_moduleSolved)
            yield break;
        for (int i = 0; i < 8; i++)
            ScreenTexts[_randomOrderB[i]].color = new Color(1, 0, 0, 1);
        for (int i = 0; i < 8; i++)
            StartCoroutine(PulseOut(i));
        yield return new WaitForSeconds(0.4f);
        _currentIx = -1;
        for (int i = 0; i < 8; i++)
            ScreenTexts[i].text = "";
        _isActivated = false;
    }

    private IEnumerator PulseIn(int ix)
    {
        var duration = 0.3f;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            ParentObjs[ix].transform.localScale = new Vector3(Mathf.Lerp(_pulsed, _size, elapsed / duration), _y, Mathf.Lerp(_pulsed, _size, elapsed / duration));
            yield return null;
            elapsed += Time.deltaTime;
        }
        ParentObjs[ix].transform.localScale = new Vector3(_size, _y, _size);
        yield break;
    }

    private IEnumerator PulseOut(int ix)
    {
        var duration = 0.3f;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            ParentObjs[ix].transform.localScale = new Vector3(Mathf.Lerp(_pulsed, 0, elapsed / duration), _y, Mathf.Lerp(_pulsed, 0, elapsed / duration));
            yield return null;
            elapsed += Time.deltaTime;
        }
        ParentObjs[ix].transform.localScale = new Vector3(0, _y, 0);
    }

    private void CheckAnswer()
    {
        Debug.LogFormat("[Type Beat #{0}] Inputted: {1}", _moduleId, _currentInput.Join(""));
        if (_currentInput.Join("") == _chosenWord)
        {
            Debug.LogFormat("[Type Beat #{0}] Module solved.", _moduleId);
            _moduleSolved = true;
            for (int i = 0; i < 8; i++)
                ScreenTexts[i].color = new Color(0, 1, 0, 1);
            Module.HandlePass();
        }
        else
        {
            Debug.LogFormat("[Type Beat #{0}] Strike.", _moduleId);
            Module.HandleStrike();
        }
    }

    private void OnGUI()
    {
        if (!_isSelected)
            return;
        Event e = Event.current;
        if (e.type != EventType.KeyDown)
            return;
        ProcessKey(e.keyCode);
    }

    private void ProcessKey(KeyCode key)
    {
        if (_currentInput[_currentIx] != "?")
            return;
        if (key >= KeyCode.A && key <= KeyCode.Z)
        {
            string k = key.ToString().ToUpperInvariant();
            _currentInput[_currentIx] = k;
            ScreenTexts[_currentIx].text = k;
        }
    }

}
